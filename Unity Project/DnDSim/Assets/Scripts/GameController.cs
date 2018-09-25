using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour {

    public bool startTransitioning;
    public bool isTransitioning;

    public static bool isMainUIOpen = true;
    public static GameObject mainUI;
    public static bool isCharacterSheetUIOpen;
    public static GameObject characterSheetUI;
    public bool isDatabaseUIOpen;
    public GameObject databaseUI;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isFirstRun") == 0) //Creates folders for storing game info if this is the first time the game is being booted
        {
            Directory.CreateDirectory(Application.persistentDataPath + @"/Characters/");
            Directory.CreateDirectory(Application.persistentDataPath + @"/Spells/");
            Directory.CreateDirectory(Application.persistentDataPath + @"/Spells/Custom Spells/");
            Directory.CreateDirectory(Application.persistentDataPath + @"/Maps/");
            PlayerPrefs.SetInt("isFirstRun", 1);
        }
    }

    // Use this for initialization
    void Start () {
        databaseUI.GetComponent<DatabaseUIController>().updateSpellsDatabase();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCharacterSheetUIOpen)
            {
                toggleCharacterSheetUI();
                toggleMainUI();
            }
            if (isDatabaseUIOpen)
            {
                toggleDatabaseUI();
                toggleMainUI();
            }
        }
	}

    public void toggleCharacterSheetUI()
    {
        if(!isTransitioning)
        {
            isCharacterSheetUIOpen = !isCharacterSheetUIOpen;
            if (isCharacterSheetUIOpen)
            {
                characterSheetUI.GetComponent<Animator>().Play("FadeIn");
                characterSheetUI.GetComponent<CharacterSheetController>().particleSys.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                characterSheetUI.GetComponent<Animator>().Play("FadeOut");
                characterSheetUI.GetComponent<CharacterSheetController>().particleSys.GetComponent<ParticleSystem>().Stop();
                //characterSheetUI.GetComponent<CharacterSheetController>().particleSys.GetComponent<ParticleSystem>().Clear();
            }
            startTransitioning = true;
        }
    }

    public void toggleDatabaseUI()
    {
        if (!isTransitioning)
        {
            isDatabaseUIOpen = !isDatabaseUIOpen;
            if (isDatabaseUIOpen)
            {
                databaseUI.GetComponent<Animator>().Play("FadeIn");
                //databaseUI.GetComponent<CharacterSheetController>().particleSys.GetComponent<ParticleSystem>().Play(); No particle system, keeping this code in case we add one
            }
            else
            {
                databaseUI.GetComponent<Animator>().Play("FadeOut");
                //databaseUI.GetComponent<CharacterSheetController>().particleSys.GetComponent<ParticleSystem>().Stop();
            }
            startTransitioning = true;
        }
    }

    public void toggleMainUI()
    {
        if(!isTransitioning)
        {
            isMainUIOpen = !isMainUIOpen;
            if (isMainUIOpen)
                mainUI.GetComponent<Animator>().Play("FadeIn");
            else
                mainUI.GetComponent<Animator>().Play("FadeOut");
            startTransitioning = true;
        }
    }

    public IEnumerator isTransitioningTimer()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(1);
        isTransitioning = false;
    }

    public void LateUpdate()
    {
        if(startTransitioning)
        {
            StartCoroutine(isTransitioningTimer());
            startTransitioning = false;
        }
    }

    public void closeApp()
    {
        characterSheetUI.GetComponent<CharacterSheetController>().serializeCharacters();
        Application.Quit();
    }
}
