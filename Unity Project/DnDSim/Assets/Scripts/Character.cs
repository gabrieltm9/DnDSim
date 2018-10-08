using System.Collections.Generic;
using UnityEngine;

public class Character {

    public string name;

    public int maxHealth;
    public int currentHealth;

    public Sprite background;

    public List<List<Spell>> spellLists = new List<List<Spell>>();
}
