using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharacterSheetController : MonoBehaviour
{
    public GameObject textureDisplay;
    void Awake()
    {
        GameController.characterSheetUI = gameObject;
    }

    private void Start()
    {
        StartCoroutine(GetSpells());
        populateSpells();
    }

    public void populateSpells()
    {
        using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
        {
            client.DownloadFile("http://dnd5e.wikia.com/wiki/Warding_Wind", @"C:\Users\gabriel.tubiomoncau\Desktop\localfile.html");
        }
   }

    IEnumerator GetSpells()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://dnd5e.wikia.com/wiki/Warding_Wind");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);
        }
        string str = www.downloadHandler.text.Substring(www.downloadHandler.text.IndexOf("Self"));
        Debug.Log(str);
    }
}
