using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterSheetController : MonoBehaviour
{
    void Awake()
    {
        GameController.characterSheetUI = gameObject;
    }

    private void Start()
    {
        StartCoroutine(GetSpells());
    }

    public void populateSpells()
    {

    }

    IEnumerator GetSpells()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://dnd5e.wikia.com/wiki/Acid_Splash");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
