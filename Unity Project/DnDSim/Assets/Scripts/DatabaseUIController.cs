using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DatabaseUIController : MonoBehaviour
{
    public GameObject gameController;
    public GameObject currentlyOpenPage;
    public GameObject currentlyOpenSubPage;

    public GameObject spellPrefab;
    public GameObject SRDSpellsScrollViewContent;

    public GameObject mainPage;

    public bool isTransitioning;

    public void updateSpellsDatabase()
    {
        foreach(Spell spell in GetComponent<DatabaseManager>().srdSpells)
            addToSRDSpellsScrollView(spell);
    }

    /*public void pullSRDSpells() OLD CODE FOR CONNECTING TO THE MySql DATABASE
    {
        MySqlConnection dbConnection = new MySqlConnection(connectionString);
        try
        {
            MySqlCommand command = dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM srdspells";
            dbConnection.Open();
            Debug.Log(dbConnection.State);
            MySqlDataReader data = command.ExecuteReader();
            while (data.Read())
                addToSRDSpellsScrollView(data["spellName"].ToString(), data["spellRange"].ToString());
            dbConnection.Close();
            Debug.Log(dbConnection.State);
        }
        catch (MySqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
        }
    }*/

    void addToSRDSpellsScrollView(Spell spell)
    {
        GameObject spellAdded = Instantiate(spellPrefab, SRDSpellsScrollViewContent.transform);
        spellAdded.transform.GetChild(0).GetComponent<Text>().text = spell.name; //Makes the instantiated button's text display the spell's name
        spellAdded.GetComponent<SpellInfo>().spellName = spell.name; //Adds the name of the spell
        spellAdded.GetComponent<SpellInfo>().intRange = spell.range; //Adds the spell's range in int form (ex: 10)
        spellAdded.GetComponent<SpellInfo>().strRange = spell.range.ToString() + " " + spell.rangeUnit; //Adds the spell's range in string form + it's unit (ex: 10 feet)
    }

    public void openSpecificPage(GameObject page)
    {
        if(currentlyOpenPage != page)
        {
            currentlyOpenPage.SetActive(false);
            currentlyOpenPage = page;
            page.SetActive(true);
        }
    }

    public void openSpecificSubPage(GameObject page) //Ex: Tabs inside of the Create page
    {
        if (currentlyOpenSubPage != page)
        {
            currentlyOpenSubPage.SetActive(false);
            currentlyOpenSubPage = page;
            page.SetActive(true);
        }
    }

    public void openDatabaseUI()
    {
        if(currentlyOpenPage != null)
            currentlyOpenPage.SetActive(false);
        currentlyOpenPage = mainPage;
        mainPage.SetActive(true);
    }
}
