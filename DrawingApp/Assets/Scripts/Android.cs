using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//References
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;
public class Android : MonoBehaviour
{
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    public InputField t_name, t_Address, t_id;
    public Text data_staff;

    public List<string> Tables = new List<string>();

    string DatabaseName = "Assets/Plugins/DrawingApp.s3db";
    // Start is called before the first frame update
    void Start()
    {
        //Application database Path android
        string filepath = System.IO.Directory.GetCurrentDirectory() + "/" + DatabaseName;
        if (!File.Exists(filepath))
        {
            // If not found on android will create Tables and database

            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/Employers");



            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/Plugins/DrawingApp.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }

        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();

        //string query;
        //query = "CREATE TABLE IF NOT EXISTS Year2021 (ID    INTEGER NOT NULL PRIMARY KEY , Cipher   VARCHAR(5000) NOT NULL, InitialMessage  VARCHAR(5000) NOT NULL,EncryptedMessage TEXT NOT NULL)";
        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
          //  dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader

        }
        catch (Exception e)
        {

            Debug.Log(e);

        }


        CreateTableNames();

        read_tableNames();
       // InsertTableName("name");
        //insert_DayTable("Y21D22M06", 20, 10, 8, 30);

        //  InsertCommand(0, 20);

        //  Debug.Log(read_blue_amount("Y21D22M06"));
    }

    public void CreateTableNames()
    {
        sqlQuery = "CREATE TABLE IF NOT EXISTS " + "TableNames" + " (name varchar(100) NOT NULL)";
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            //  reader = dbcmd.ExecuteReader();
            dbconn.Close();
        }
        //data_staff.text = "";
        Debug.Log("Insert Done  ");

       // InsertCommand(table, blue_amount, yellow_amount, red_amount, green_amount);
    }



    public void insert_DayTable(string table, int blue_amount, int yellow_amount, int red_amount, int green_amount)
    {
        sqlQuery = "CREATE TABLE IF NOT EXISTS " + table + " (blue_amount INTEGER(" + blue_amount + ") NOT NULL, yellow_amount INTEGER(" + yellow_amount + ") NOT NULL, red_amount INTEGER(" + red_amount + ") NOT NULL, green_amount INTEGER(" + green_amount + ") NOT NULL)";
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
          //  reader = dbcmd.ExecuteReader();
            dbconn.Close();
        }
        //data_staff.text = "";
        Debug.Log("Insert Done  ");

        InsertCommand(table, blue_amount, yellow_amount, red_amount, green_amount);
    }

    public void InsertTableName(string name)
    {
        if (!Tables.Contains(name))
        {
            //sqlQuery = "INSERT INTO " + table + " ( blue_amount) VALUES ( " + blue_amount + ")," + "( yellow_amount) VALUES ( " + yellow_amount + ")," + "( red_amount) VALUES ( " + red_amount + ")," + "( green_amount) VALUES ( " + green_amount + ")";
            sqlQuery = "INSERT INTO TableNames (name) VALUES (\"" + name + "\")";
            using (dbconn = new SqliteConnection(conn))
            {
                dbconn.Open(); //Open connection to the database.
                dbcmd = dbconn.CreateCommand();
                dbcmd.CommandText = sqlQuery;
                // dbcmd.ExecuteScalar();
                reader = dbcmd.ExecuteReader();
                dbconn.Close();
            }
            Tables.Add(name);
        }
    }


    void InsertCommand(string table, int blue_amount, int yellow_amount, int red_amount, int green_amount)
    {
        if (!Tables.Contains(table))
        {

            sqlQuery = "INSERT INTO " + table + 
                "(blue_amount, yellow_amount, red_amount, green_amount) VALUES ( " + blue_amount 
                + "," + yellow_amount + "," + red_amount + "," + green_amount + ")";
            using (dbconn = new SqliteConnection(conn))
            {
                dbconn.Open(); //Open connection to the database.
                dbcmd = dbconn.CreateCommand();
                dbcmd.CommandText = sqlQuery;
                // dbcmd.ExecuteScalar();
                reader = dbcmd.ExecuteReader();
                dbconn.Close();
            }
            InsertTableName(table);
        }
    }

    //Read All Data For To Database
    private int get_tableNameSize()
    {
        int result = 0;


        using (dbconn = new SqliteConnection(conn))
        {

            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT COUNT(name) FROM TableNames";// table name
            dbcmd.CommandText = sqlQuery;
            // IDataReader reader = dbcmd.ExecuteReader();
            result = Convert.ToInt32(dbcmd.ExecuteScalar());
            //while (reader.Read())
            //{
            //    // idreaders = reader.GetString(1);
            //    result = reader.GetString(0);


            //    //Addressreaders = reader.GetString(1);
            //}
            //reader.Close();
            //reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //       dbconn = null;

            return result;

        }
    }

    public string[] read_tableNames()
    {
        int indexCount = get_tableNameSize();
        string[] result = new string[indexCount];

        int currIndex = 0;


        using (dbconn = new SqliteConnection(conn))
        {
            

            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name FROM TableNames";// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                // idreaders = reader.GetString(1);
                // result = reader.GetString(0);
                //for (int i = 0; i < indexCount; i++)
                //{
                //    result[i] = reader.GetString(i);
                //}

                result[currIndex] = reader.GetValue(0).ToString();
                currIndex++;
                
                //Addressreaders = reader.GetString(1);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //       dbconn = null;
            for (int i = 0; i < result.Length; i++)
            {
                Tables.Add(result[i]);
            }

            return result;

        }
    }

    public int[] read_color(string day)
    {           

        using (dbconn = new SqliteConnection(conn))
        {
            int[] result = new int[4];
            int currResult = -1;

            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT blue_amount FROM " + day;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                currResult++;
                // idreaders = reader.GetString(1);
                if(currResult < 4) result[currResult] = reader.GetInt32(currResult);
                
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //       dbconn = null;

            return result;

        }
    }

    private int read_blue_amount(string day)
    {
        int result = 5;


        using (dbconn = new SqliteConnection(conn))
        {          

            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT blue_amount FROM " + day;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                // idreaders = reader.GetString(1);
                result = reader.GetInt32(0);
                //Addressreaders = reader.GetString(1);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //       dbconn = null;

            return result;

        }
    }
    //Search on Database by ID
    private void Search_function(string Search_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string Name_readers_Search, Address_readers_Search;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,address " + "FROM Staff where id =" + Search_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                //  string id = reader.GetString(0);
                Name_readers_Search = reader.GetString(0);
                Address_readers_Search = reader.GetString(1);
                data_staff.text += Name_readers_Search + " - " + Address_readers_Search + "\n";

                Debug.Log(" name =" + Name_readers_Search + "Address=" + Address_readers_Search);

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }


    //Search on Database by ID
    private void F_to_update_function(string Search_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string Name_readers_Search, Address_readers_Search;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,address " + "FROM Staff where id =" + Search_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {

                Name_readers_Search = reader.GetString(0);
                Address_readers_Search = reader.GetString(1);
                t_name.text = Name_readers_Search;
                t_Address.text = Address_readers_Search;

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }
    //Update on  Database 
    private void update_function(string update_id, string update_name, string update_address)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE Staff set name = @name ,address = @address where ID = @id ");

            SqliteParameter P_update_name = new SqliteParameter("@name", update_name);
            SqliteParameter P_update_address = new SqliteParameter("@address", update_address);
            SqliteParameter P_update_id = new SqliteParameter("@id", update_id);

            dbcmd.Parameters.Add(P_update_name);
            dbcmd.Parameters.Add(P_update_address);
            dbcmd.Parameters.Add(P_update_id);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
            Search_function(t_id.text);
        }

        // SceneManager.LoadScene("home");
    }



    //Delete
    private void Delete_function(string Delete_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {

            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "DELETE FROM Staff where id =" + Delete_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();


            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            data_staff.text = Delete_by_id + " Delete  Done ";

        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}