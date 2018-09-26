using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenScript : MonoBehaviour {
    private Character character;//the character the token corresponds to
    private GameObject characterGO;//character GameObject
    private GameObject CharacterSheet;//the UI Character sheet

    private PlayerController owner;//will find the playerController class on the camera

    private void Start()
    {
        owner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>();
    }

    private void OnMouseDown()//clicking on token enables and disables the character sheet
    {
        Debug.Log("Token Clicked");
        if (characterGO.activeInHierarchy == false)
            characterGO.SetActive(true);
        else
            characterGO.SetActive(false);
    }

}
