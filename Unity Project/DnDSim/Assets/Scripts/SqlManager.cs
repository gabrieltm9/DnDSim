﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

public class SqlManager : MonoBehaviour {

    static string connectionString = "Server=dndsimsql.mysql.database.azure.com; Port=3306; Database=spells; Uid=tunabandit@dndsimsql; Pwd=Iteam2015;";

    public static void testAddSpell(string spellName, string spellRange)
    {
        MySqlConnection dbConnection = new MySqlConnection(connectionString);
        try
        {
            MySqlCommand command = dbConnection.CreateCommand();
            command.CommandText = "INSERT INTO srdspells (spellName, spellRange) VALUES ('" + spellName + "','" + spellRange + "');";
            dbConnection.Open();
            Debug.Log(dbConnection.State);
            command.ExecuteNonQuery();
            dbConnection.Close();
            Debug.Log(dbConnection.State);
        }
        catch (MySqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
        }
    }
}
