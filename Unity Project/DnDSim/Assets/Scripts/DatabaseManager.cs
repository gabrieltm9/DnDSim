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
        //Spell spell = new Spell();
        //srdSpells.Add(spell);
        //XMLSerializer.Serialize(srdSpells, Application.persistentDataPath + @"/Spells/srdspells.xml");
        loadSpellFiles();
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
}
