using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DatabaseUIController : MonoBehaviour
{
    public GameObject gameController;
    public GameObject currentlyOpenPage;
    string connectionString = "Server=dndsimsql.mysql.database.azure.com; Port=3306; Database=spells; Uid=tunabandit@dndsimsql; Pwd=Iteam2015;";

    public GameObject spellPrefab;
    public GameObject SRDSpellsScrollViewContent;

    public bool isTransitioning;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateSpellsDatabase()
    {
        pullSRDSpells();
    }

    public void pullSRDSpells()
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
    }

    void addToSRDSpellsScrollView(string name, string range)
    {
        GameObject spellAdded = Instantiate(spellPrefab, SRDSpellsScrollViewContent.transform);
        spellAdded.transform.GetChild(0).GetComponent<Text>().text = name;
        spellAdded.GetComponent<SpellInfo>().spellName = name;
        spellAdded.GetComponent<SpellInfo>().strRange = range;
        if (range.IndexOf(" ") > 0)
            spellAdded.GetComponent<SpellInfo>().intRange = int.Parse(range.Substring(0, (range.IndexOf(" "))));
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
}
