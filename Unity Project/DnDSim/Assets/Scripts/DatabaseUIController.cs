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
    public GameObject createSpellSubPage; //Also used to edit spells

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
        spellAdded.GetComponent<SpellInfo>().spell = spell;
    }

    public void openSpecificPage(GameObject page)
    {
        if(currentlyOpenPage != page)
        {
            if (currentlyOpenPage != null)
            {
                currentlyOpenPage.SetActive(false);
                currentlyOpenPage = null;
            }
            currentlyOpenPage = page;
            page.SetActive(true);
        }
    }

    public void openSpecificSubPage(GameObject page) //Ex: Tabs inside of the Create page
    {
        if (currentlyOpenSubPage != page)
        {
            if(currentlyOpenSubPage != null)
            {
                currentlyOpenSubPage.SetActive(false);
                currentlyOpenSubPage = null;
            }
            currentlyOpenSubPage = page;
            page.SetActive(true);
        }
    }

    public void openDatabaseUI()
    {
        if (currentlyOpenPage != null)
        {
            currentlyOpenPage.SetActive(false);
            currentlyOpenPage = null;
        }
        if (currentlyOpenSubPage != null)
        {
            currentlyOpenSubPage.SetActive(false);
            currentlyOpenSubPage = null;
        }
        currentlyOpenPage = mainPage;
        mainPage.SetActive(true);
    }

    public void saveSpellToDatabase()
    {
        int temp;
        //Creates spell
        Spell spell = new Spell();
        spell.name = createSpellSubPage.transform.GetChild(0).GetComponent<InputField>().text; //Sets name
        spell.level = createSpellSubPage.transform.GetChild(1).GetComponent<Dropdown>().value; //Sets level
        spell.description = createSpellSubPage.transform.GetChild(13).GetComponent<InputField>().text; //Sets description

        if (int.TryParse(createSpellSubPage.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text, out temp)) //Checks if the range has been inputted (if not this would throw an int parsing error)
            spell.range = int.Parse(createSpellSubPage.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text); //Sets range
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s range. Is it missing? Skipping"); //Logs message warning that the spell's range could not be parsed
        if (createSpellSubPage.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<Text>().text == "Custom") //If it has a custom range
            spell.rangeUnit = createSpellSubPage.transform.GetChild(6).GetChild(3).GetChild(1).GetComponent<Text>().text; //Set the range unit to the custom text
        else //If the spell isnt custom
            spell.rangeUnit = createSpellSubPage.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<Text>().text; //Set the range unit to the dropdown range selected

        if (createSpellSubPage.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Text>().text == "Custom") //If it has a custom effect range
            spell.effectRangeDescription = createSpellSubPage.transform.GetChild(7).GetChild(3).GetChild(0).GetComponent<Text>().text; //Sets the effectRangeDescription to the custom effect range inputed (NOTE: This variable is the only one used out of all effect range vars if a custom effect range is inputed)
        else //If it doesnt have a custom effect range
        {
            spell.effectRangeDescription = createSpellSubPage.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Text>().text; //Sets the effect range description (ex. single target)
            if (spell.effectRangeDescription == "Area of Effect") //If it is an area of effect spell
            {
                if (int.TryParse(createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the effect range has been inputted (if not this would throw an int parsing error)
                    spell.effectRange = int.Parse(createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text); //Sets effect range
                else
                    Debug.LogWarning("Could not parse spell '" + spell.name + "'s AOE effect range. Is it missing? Skipping"); //Logs message warning that the spell's effect range could not be parsed
                if (int.TryParse(createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the effect range size has been inputted (if not this would throw an int parsing error)
                    spell.effectRangeSize = int.Parse(createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>().text); //Sets effect range size
                else
                    Debug.LogWarning("Could not parse spell '" + spell.name + "'s AOE effect range size (width). Is it missing? Skipping"); //Logs message warning that the spell's effect range size could not be parsed
                if (int.TryParse(createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(3).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the number of targets has been inputted (if not this would throw an int parsing error)
                    spell.numberOfTargets = int.Parse(createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(3).GetChild(1).GetComponent<Text>().text); //Sets number of targets
                else
                    Debug.LogWarning("Could not parse spell '" + spell.name + "'s AOE number of targets. Is it missing? Skipping"); //Logs message warning that the spell's effect range could not be parsed
                                                                                                                                   //spell.effectRangeSizeUnit = CURRENTLY NOT CUSTOMIZABLE IN GAME. Ex: in '100 foot line that is 5 feet wide' this would be the 'feet wide'. 'feet wide' is the default
                spell.effectRangeUnit = createSpellSubPage.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text;
            }
            else
            {
                if (int.TryParse(createSpellSubPage.transform.GetChild(7).GetChild(1).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the effect range has been inputted (if not this would throw an int parsing error)
                    spell.effectRange = int.Parse(createSpellSubPage.transform.GetChild(7).GetChild(1).GetChild(1).GetComponent<Text>().text); //Sets effect range
                else
                    Debug.LogWarning("Could not parse spell '" + spell.name + "'s effect range. Is it missing? Skipping"); //Logs message warning that the spell's effect range could not be parsed
                if (spell.effectRangeDescription == "Multiple Targets")
                {
                    if (int.TryParse(createSpellSubPage.transform.GetChild(7).GetChild(1).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the number of targets has been inputted (if not this would throw an int parsing error)
                        spell.numberOfTargets = int.Parse(createSpellSubPage.transform.GetChild(7).GetChild(1).GetChild(1).GetComponent<Text>().text); //Sets number of targets
                    else
                        Debug.LogWarning("Could not parse spell '" + spell.name + "'s number of targets. Is it missing? Skipping"); //Logs message warning that the spell's effect range could not be parsed
                }
            }
        }

        spell.isRitual = createSpellSubPage.transform.GetChild(4).GetComponent<Toggle>().isOn;
        spell.cComponent = createSpellSubPage.transform.GetChild(8).GetComponent<Toggle>().isOn;
        spell.vComponent = createSpellSubPage.transform.GetChild(9).GetComponent<Toggle>().isOn;
        spell.sComponent = createSpellSubPage.transform.GetChild(10).GetComponent<Toggle>().isOn;
        spell.mComponentDescription = createSpellSubPage.transform.GetChild(11).GetChild(1).GetComponent<Text>().text;

        if (int.TryParse(createSpellSubPage.transform.GetChild(14).GetChild(0).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the base number of dice has been inputted (if not this would throw an int parsing error)
            spell.baseNumberOfDamageDice = int.Parse(createSpellSubPage.transform.GetChild(15).GetChild(0).GetChild(1).GetComponent<Text>().text);
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s base number of damage dice. Is it missing? Skipping"); //Logs message warning that the spell's base number of damage dice could not be parsed
        if (int.TryParse(createSpellSubPage.transform.GetChild(14).GetChild(1).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the base type of dice has been inputted (if not this would throw an int parsing error)
            spell.typeOfDamageDice = int.Parse(createSpellSubPage.transform.GetChild(15).GetChild(1).GetChild(1).GetComponent<Text>().text);
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s base number of damage dice. Is it missing? Skipping"); //Logs message warning that the spell's base type of damage dice could not be parsed
        if (int.TryParse(createSpellSubPage.transform.GetChild(14).GetChild(2).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the base number of dice has been inputted (if not this would throw an int parsing error)
            spell.diceNumberSpellSlotIncrease = int.Parse(createSpellSubPage.transform.GetChild(15).GetChild(2).GetChild(1).GetComponent<Text>().text);
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s increasing number of damage dice. Is it missing? Skipping"); //Logs message warning that the spell's increasing number of damage dice could not be parsed
        if (int.TryParse(createSpellSubPage.transform.GetChild(14).GetChild(3).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the base type of dice has been inputted (if not this would throw an int parsing error)
            spell.typeOfDiceSpellSlotIncrease = int.Parse(createSpellSubPage.transform.GetChild(15).GetChild(3).GetChild(1).GetComponent<Text>().text);
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s increasing type of damage dice. Is it missing? Skipping"); //Logs message warning that the spell's increasing type of damage dice could not be parsed

        if (int.TryParse(createSpellSubPage.transform.GetChild(12).GetChild(1).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the duration has been inputted (if not this would throw an int parsing error)
            spell.duration = int.Parse(createSpellSubPage.transform.GetChild(12).GetChild(1).GetChild(1).GetComponent<Text>().text);
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s duration. Is it missing? Skipping"); //Logs message warning that the spell's duration could not be parsed
        spell.durationUnit = createSpellSubPage.transform.GetChild(12).GetChild(0).GetChild(0).GetComponent<Text>().text;

        if (int.TryParse(createSpellSubPage.transform.GetChild(5).GetChild(1).GetChild(1).GetComponent<Text>().text, out temp)) //Checks to see if the casting time has been inputted (if not this would throw an int parsing error)
            spell.castingTime = int.Parse(createSpellSubPage.transform.GetChild(5).GetChild(1).GetChild(1).GetComponent<Text>().text);
        else
            Debug.LogWarning("Could not parse spell '" + spell.name + "'s casting time. Is it missing? Skipping"); //Logs message warning that the spell's casting time could not be parsed
        spell.castingTimeUnit = createSpellSubPage.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>().text;

        spell.school = createSpellSubPage.transform.GetChild(2).GetChild(0).GetComponent<Text>().text;

        spell.savingThrow = createSpellSubPage.transform.GetChild(15).GetChild(0).GetComponent<Text>().text;

        //Saves spell
        GetComponent<DatabaseManager>().srdSpells.Add(spell);
    }
}
