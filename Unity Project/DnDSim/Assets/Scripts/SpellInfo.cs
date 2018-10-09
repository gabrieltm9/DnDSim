using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInfo : MonoBehaviour {
    public Spell spell;

    public void displaySpellStats() //When this spell is clicked, this runs
    {
        GameObject.FindGameObjectWithTag("Database").GetComponent<DatabaseUIController>().displaySpellStats(spell);
    }
}
