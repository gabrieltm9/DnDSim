using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;

public class DatabaseManager : MonoBehaviour {

    public List<Spell> srdSpells = new List<Spell>();
    public List<Spell> customSpells = new List<Spell>();

    private void Start()
    {
        //Spell spell = new Spell("Fireball", 3, "A bright streak flashes from your pointing finger to a point you choose within range and then blossoms with a low roar into an explosion of flame. Each creature in a 20-foot-radius sphere centered on that point must make a Dexterity saving throw. A target takes 8d6 fire damage on a failed save, or half as much damage on a successful one. The fire spreads around corners. It ignites flammable objects in the area that aren’t being worn or carried.", 150, "feet", 20, "foot-radius", 0, true, true, true, false, "A tiny ball of bat guano and sulfur", 8, 6, 1, 0, "", 1, "action", "Evocation", new List<string> {"Sorcerer, Wizard"}, false, "Dexterity");
        //srdSpells.Add(spell);
        //XMLSerializer.Serialize(srdSpells, Application.persistentDataPath + @"/Spells/srdspells.xml");

        loadSpellFiles();
        GetComponent<DatabaseUIController>().updateSpellsDatabase();
    }

    public void loadSpellFiles()
    {
        srdSpells = XMLSerializer.Deserialize<List<Spell>>(Application.persistentDataPath + @"/Spells/SRDSpells.xml");
        if (customSpells.Count > 0)
            customSpells.Clear();
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath + @"/Spells/Custom Spells/"))
        {
            if (file.Substring(file.Length - 4) == ".xml") //If the file is an xml file
            {
                Spell spell = XMLSerializer.Deserialize<Spell>(file);
                if (!customSpells.Contains(spell))
                    customSpells.Add(spell);
            }
            
        }
    }

    public void serializeSpellLists()
    {
        XMLSerializer.Serialize(srdSpells, Application.persistentDataPath + @"/Spells/srdspells.xml");
    }
}
