using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public class DataBase
    {
    //private const string fileName = "BDGame.bytes";
    private const string fileName = "database.db";
    public string DBPath;
    public  SqliteConnection connection = new SqliteConnection();
    private SqliteCommand command = new SqliteCommand();

        public DataBase()
        {
            DBPath = GetDatabasePath();
        }

        private  string GetDatabasePath()
        {
        return Path.Combine(Application.streamingAssetsPath, fileName);
        //return Path.Combine(Application.dataPath, fileName);
        //#if UNITY_EDITOR
        //return Path.Combine(Application.streamingAssetsPath, fileName);
        //#if UNITY_STANDALONE
        //string filePath = Path.Combine(Application.dataPath, fileName);
        //if (!File.Exists(filePath)) UnpackDatabase(filePath);
        //return filePath;
        //#elif UNITY_ANDROID
        //string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //if(!File.Exists(filePath)) UnpackDatabase(filePath);
        //return filePath;
        //#endif
    }
        //#endif

    [System.Obsolete]
        private  void UnpackDatabase(string toPath)
        {
        string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);
        //string fromPath = Path.Combine(Application.dataPath, fileName);

        WWW reader = new WWW(fromPath);
            while (!reader.isDone) { }

            File.WriteAllBytes(toPath, reader.bytes);
        }

        public  void OpenConnection()
        {
            connection = new SqliteConnection("Data Source=" + DBPath);
            command = new SqliteCommand(connection);
            connection.Open();
        }

        public  void CloseConnection()
        {
            connection.Close();
            command.Dispose();
        }

        /// <summary> Этот метод выполняет запрос query. </summary>
        /// <param name="query"> Собственно запрос. </param>
        public  void ExecuteQueryWithoutAnswer(string query)
        {
            OpenConnection();
            command.CommandText = query;
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public  string ExecuteQueryWithAnswer(string query)
        {
            OpenConnection();
            command.CommandText = query;
            var answer = command.ExecuteScalar();
            CloseConnection();

            if (answer != null) return answer.ToString();
            else return null;
        }

    public void ExecuteNonQuery(string query)
    {
        //OpenConnection();
        //command = new SqliteCommand(query, connection);
        ////connection.Open();
        //command.CommandText = query;
        //command.ExecuteNonQuery();
        //CloseConnection();
        using (var connection = new SqliteConnection(GetDatabasePath()))
        {
            using (var command = new SqliteCommand(query, connection))
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public  DataTable GetTable(string query)
        {
            OpenConnection();

            SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

            DataSet DS = new DataSet();
            adapter.Fill(DS);
            adapter.Dispose();

            CloseConnection();

            return DS.Tables[0];
        }
}

