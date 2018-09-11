using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Character {

    public string name;

    public int maxHealth;
    public int currentHealth;

    public Sprite background;

    public List<string> spells = new List<string>();
}
