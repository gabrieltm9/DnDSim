using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;

public class CharacterSheetController : MonoBehaviour
{
    public GameObject gameController;
    public GameObject particleSys;
    public GameObject mainPage;
    public bool isTransitioning;

    public List<Character> characters = new List<Character>();

    public GameObject characterForMainPagePrefab;
    public GameObject spellForSpellListPrefab;

    public GameObject characterViewerPages;
    public GameObject characterViewerUI;
    public GameObject characterViewerNamePlaceholder;
    public Character characterBeingEdited;
    public bool isACharacterBeingEdited;
    public GameObject currentlyOpenPage;

    public bool shouldUpdateSpellList;


    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        GameController.characterSheetUI = gameObject;
        foreach(Character charac in characters)
        {
            Debug.Log(charac.name);
        }
    }

    private void LateUpdate()
    {
        if(shouldUpdateSpellList)
        {
            updateSpellsPage();
            shouldUpdateSpellList = false;
        }
    }

    private void Start()
    {
        Directory.CreateDirectory(Application.persistentDataPath + @"/Characters/");
        loadCharacters();
    }

    IEnumerator GetSpellDescription(string name)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://dnd5e.wikia.com/wiki/" + fixSpellName(name));
        yield return www.SendWebRequest();
        string str = "";

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            str = "Could not find spell by that name";
        }
        else
        {
            int first = www.downloadHandler.text.IndexOf("og:description") + 25;
            int length = (www.downloadHandler.text.IndexOf(">", first) - first - 3);
            if (length > 0)
                str = www.downloadHandler.text.Substring(first, length);
            else
                str = "Could not find description";
            if (currentlyOpenPage != null && currentlyOpenPage.name == "SpellsPage")
                currentlyOpenPage.transform.GetChild(2).GetComponent<Text>().text = str;
        }
    }

    IEnumerator AddSpellToList(string name, Character character)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://dnd5e.wikia.com/wiki/" + fixSpellName(name));
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string str = "";
            int first = www.downloadHandler.text.IndexOf("og:title") + 19;
            int length = (www.downloadHandler.text.IndexOf(">", first) - first - 3);
            if (length > 0)
            {
                str = www.downloadHandler.text.Substring(first, length);
                character.spells.Add(str);
                GameObject content = currentlyOpenPage.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
                foreach (Transform child in content.transform)
                {
                    Destroy(child.gameObject);
                }
                shouldUpdateSpellList = true;
            }
            else
                str = "Could not find spell by that name";
        }
    }

    public string fixSpellName(string str) //Properly formulates the spell's name so it can be
    {
        string fixedName = "";
        char[] array = str.ToLower().ToCharArray();
        for(int x = 0; x < array.Length; x++)
        {
            if (array[x].Equals(' '))
            {
                fixedName += "_";
            }
            else if (x == 0 || array[x - 1].Equals(' '))
            {
                fixedName += array[x].ToString().ToUpper();
            }
            else
            {
                fixedName += array[x].ToString(); //Hi_Moaoma_to_Owlol
            }
        }
        array = fixedName.ToCharArray();
        //Exceptions
        while (fixedName.IndexOf("Of") > 0 && array[fixedName.IndexOf("Of") - 1].Equals('_'))
        {
            fixedName = fixedName.Substring(0, fixedName.IndexOf("Of")) + "of" + fixedName.Substring(fixedName.IndexOf("Of") + 2);
        }
        while (fixedName.IndexOf("The") > 0 && array[fixedName.IndexOf("The") - 1].Equals('_'))
        {
            fixedName = fixedName.Substring(0, fixedName.IndexOf("The")) + "the" + fixedName.Substring(fixedName.IndexOf("The") + 3);
        }
        while (fixedName.IndexOf("And") > 0 && array[fixedName.IndexOf("And") - 1].Equals('_'))
        {
            fixedName = fixedName.Substring(0, fixedName.IndexOf("And")) + "and" + fixedName.Substring(fixedName.IndexOf("And") + 3);
        }
        while (fixedName.IndexOf("From") > 0 && array[fixedName.IndexOf("From") - 1].Equals('_'))
        {
            fixedName = fixedName.Substring(0, fixedName.IndexOf("From")) + "from" + fixedName.Substring(fixedName.IndexOf("From") + 4);
        }
        return fixedName;
    }

    public void viewCharacter(Character character)
    {
        characterBeingEdited = character;
        toggleCharacterPages();
        updateAllCharacterPages();
    }

    public void updateAllCharacterPages()
    {
        updateUIForPages();
        updateStatsPage();
        updateSpellsPage();
    }

    public void updateSpecificPage(GameObject pageToOpen)
    {
        switch(pageToOpen.name)
        {
            case "SpellsPage":
                updateSpellsPage();
                break;
        }
    }

    public void toggleCharacterPages()
    {
        if (!isTransitioning)
        {
            mainPage.SetActive(true);
            characterViewerPages.SetActive(true);
            isACharacterBeingEdited = !isACharacterBeingEdited;
            if (isACharacterBeingEdited)
            {
                mainPage.GetComponent<Animator>().Play("FadeOut");
                characterViewerPages.transform.parent.GetComponent<Animator>().Play("FadeIn");
                characterViewerPages.transform.GetChild(0).gameObject.SetActive(true);
                currentlyOpenPage = characterViewerPages.transform.GetChild(0).gameObject;
                StartCoroutine(disablePartOfUIDelay(0));
            }
            else
            {
                mainPage.GetComponent<Animator>().Play("FadeIn");
                characterViewerPages.transform.parent.GetComponent<Animator>().Play("FadeOut");
                StartCoroutine(disablePartOfUIDelay(1));
            }
        }
    }

    public void loadCharacters()
    {
        GameObject scrollViewContent = mainPage.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath + @"/Characters/"))
        {
            Character character = XMLSerializer.Deserialize<Character>(file);
            characters.Add(character);

            GameObject newCharac = Instantiate(characterForMainPagePrefab, scrollViewContent.transform);
            newCharac.GetComponent<CharacterDataScript>().character = character;
            if(scrollViewContent.transform.childCount > 1)
                newCharac.GetComponent<RectTransform>().localPosition = new Vector3(scrollViewContent.transform.GetChild(scrollViewContent.transform.childCount - 2).GetComponent<RectTransform>().localPosition.x + 600, newCharac.GetComponent<RectTransform>().localPosition.y, newCharac.GetComponent<RectTransform>().localPosition.z);
            //newCharac.transform.GetChild(0).GetComponent<Image>().sprite = //CHANGES THE BACKGROUND 
            newCharac.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = character.name;

            //Character Shifter (expands scroll view and repositions characters accordingly)
            scrollViewContent.GetComponent<RectTransform>().offsetMax = new Vector2(scrollViewContent.GetComponent<RectTransform>().offsetMax.x + 600, scrollViewContent.GetComponent<RectTransform>().offsetMax.y);
            for(int x = 0; x < scrollViewContent.transform.childCount; x++)
            {
                scrollViewContent.transform.GetChild(x).transform.localPosition = new Vector3(scrollViewContent.transform.GetChild(x).transform.localPosition.x - 300, scrollViewContent.transform.GetChild(x).transform.localPosition.y, scrollViewContent.transform.GetChild(x).transform.localPosition.z);
            }
        }
    }

    public void updateUIForPages()
    {
        characterViewerNamePlaceholder.GetComponent<Text>().text = characterBeingEdited.name;
    }

    public void updateStatsPage()
    {
        
    }

    public void updateSpellsPage()
    {
        if (currentlyOpenPage.name == "SpellsPage")
        {
            GameObject content = currentlyOpenPage.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, -80);
            foreach (string spell in characterBeingEdited.spells)
            {
                GameObject newSpell = Instantiate(spellForSpellListPrefab, content.transform);
                newSpell.GetComponent<Text>().text = spell;
                if (content.transform.childCount > 1)
                    newSpell.transform.localPosition = new Vector3(newSpell.transform.localPosition.x, content.transform.GetChild(content.transform.childCount - 2).transform.localPosition.y - 60, newSpell.transform.localPosition.z);
                //Spells Shifter (expands the viewer and shifts spells up accordingly)
                //content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + 160);
                content.GetComponent<RectTransform>().offsetMin = new Vector2(content.GetComponent<RectTransform>().offsetMin.x, content.GetComponent<RectTransform>().offsetMin.y - 80);
                for (int x = 0; x < content.transform.childCount; x++)
                {
                    content.transform.GetChild(x).transform.localPosition = new Vector3(content.transform.GetChild(x).transform.localPosition.x, content.transform.GetChild(x).transform.localPosition.y + 20, content.transform.GetChild(x).transform.localPosition.z);
                }
            }
        }
    }

    public void openSpecificCharacterPage(GameObject page)
    {
        if(currentlyOpenPage != page)
        {
            if(currentlyOpenPage.name == "SpellsPage")
            {
                GameObject content = currentlyOpenPage.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
                foreach (Transform child in content.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            page.SetActive(true);
            currentlyOpenPage.SetActive(false);
            currentlyOpenPage = page;
            updateSpecificPage(page);
        }
    }

    public void serializeCharacters()
    {
        foreach (Character charac in characters)
        {
            XMLSerializer.Serialize(charac, Application.persistentDataPath + @"/Characters/" + charac.name + ".xml");
        }
    }

    public void changeNameInputField(InputField input)
    {
        string oldName = characterBeingEdited.name;
        System.IO.File.Move(Application.persistentDataPath + @"/Characters/" + oldName + ".xml", Application.persistentDataPath + @"/Characters/" + input.text + ".xml"); //Updates the serialized file so the character is not duplicated under a new name
        characters.Remove(characterBeingEdited);
        characterBeingEdited.name = input.text;
        characters.Add(characterBeingEdited);
        characterViewerNamePlaceholder.GetComponent<Text>().text = characterBeingEdited.name;
    }

    public IEnumerator disablePartOfUIDelay(int id)
    {
        isTransitioning = true;
        yield return new WaitForSeconds(1);
        disablePartOfUI(id);
        isTransitioning = false;
    }

    public void disablePartOfUI(int id)
    {
        switch(id)
        {
            case 0:
                mainPage.SetActive(false);
                break;
            case 1:
                characterViewerPages.SetActive(false);
                break;
            case 2:
                characterViewerUI.SetActive(false);
                break;
            case 3:
                currentlyOpenPage.SetActive(false);
                break;
                
        }
    }

    public void displaySpellDescription(GameObject spellObj)
    {
        StartCoroutine(GetSpellDescription(spellObj.GetComponent<Text>().text));
    }
    
    public void addSpell(InputField input)
    {
        StartCoroutine(AddSpellToList(input.text, characterBeingEdited));
    }

    public void newCharacter()
    {
        Character character = new Character();
        character.name = "Jameson";
        character.maxHealth = 12;
        character.currentHealth = 12;
        characterBeingEdited = character;
        characters.Add(character);
        toggleCharacterPages();
        updateAllCharacterPages();
    }
}