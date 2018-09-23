using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellReferenceScript : MonoBehaviour {
    
	public void getSpellDescription()
    {
        GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterSheetController>().displaySpellDescription(gameObject);
    }
}
