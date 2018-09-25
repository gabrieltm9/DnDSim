using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenScript : MonoBehaviour {
    private Character character;//the character the token corresponds to
    private GameObject characterGO;//character GameObject
    private GameObject CharacterSheet;

    private void Start()
    {
        character = transform.parent.gameObject.GetComponent<Character>();
    }

    private void OnMouseDown()//clicking on token enables and disables the character sheet
    {
        if (characterGO.activeInHierarchy == false)
            characterGO.SetActive(true);
        else
            characterGO.SetActive(false);
    }

}
