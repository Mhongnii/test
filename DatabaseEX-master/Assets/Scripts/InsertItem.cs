using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;
using UnityEngine.UI;

public class InsertItem : MonoBehaviour {

	public InputField NameInput;
	public InputField PasswordInput;
	public InputField AgeInput;
	public Text scrollview;

	public void Insert(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);

		dbconn.Open(); //Open connection to the database.

		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "insert Into Account (Username,Password,Age) Values ('" + NameInput.text + "','" + PasswordInput.text + "','" + AgeInput.text + "')";
	
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteNonQuery();
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}

	public void Select(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);

		dbconn.Open(); //Open connection to the database.

		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "SELECT * FROM Account";
		dbcmd.CommandText = sqlQuery;

		IDataReader reader = dbcmd.ExecuteReader();
		scrollview.text = " ";
		while (reader.Read ()) {
			String Username = reader.GetString (1);
			String Password = reader.GetString (2);
			int Age = reader.GetInt32 (3);

			scrollview.text += Username + "-" + Password + "-" + Age + "\n";
		}

		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}
}
