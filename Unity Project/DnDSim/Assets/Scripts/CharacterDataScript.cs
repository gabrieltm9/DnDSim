using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataScript : MonoBehaviour {

    public GameObject characterManager;
    public Character character; //Who this character is

    public void Start()
    {
        characterManager = GameObject.FindGameObjectWithTag("CharacterManager");
    }

    public void loadThisCharacted()
    {
        characterManager.GetComponent<CharacterSheetController>().viewCharacter(character);
    }
}
