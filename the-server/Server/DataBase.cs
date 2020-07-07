using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    public class DataBase
    {
        public static string ConnectionString = @"server=localhost;user=root;database=penetration;password=1234;", query = string.Empty;
        public static MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public static string getWordbooks()
        {
            query = "SELECT `name` FROM `wordbook`";
            MySqlCommand command = new MySqlCommand(query, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            string wordbooks = string.Empty;
            while (reader.Read())
            {
                wordbooks += reader[0].ToString() + ",";
            }
            reader.Close();
            return wordbooks;
        }
        public static string registration(string name, string login, string password)
        {
            try
            {
                query = $"SELECT `id` FROM `users` WHERE `login` = '{login}'";
                MySqlCommand command = new MySqlCommand(query, Connection);
                if (command.ExecuteScalar() == null)
                {
                    query = $"INSERT INTO `users` (`name`,`login`,`password`) VALUES('{name}','{login}','{password}')";
                    MySqlCommand command1 = new MySqlCommand(query, Connection);
                    command1.ExecuteNonQuery();
                    return "OKreg";
                }
                else return "Failreg";
            }
            catch (Exception ex) { return ex.ToString(); };
        }
        public static string authorization(string login, string password)
        {
            try
            {
                query = $"SELECT `id` FROM `users` WHERE `login` = '{login}' AND `password` = '{password}'";
                MySqlCommand command = new MySqlCommand(query, Connection);
                if (command.ExecuteScalar() == null) return "Failauth";
                else return "OKauth";
            }
            catch (Exception ex) { return ex.ToString(); }
        }
        public static string addWordbook(string[] words, string nameWordbook, string login)
        {
            try
            {
                query = $"SELECT `id` FROM `users` where login = '{login}'";
                MySqlCommand command = new MySqlCommand(query, Connection);
                int id = int.Parse(command.ExecuteScalar().ToString());
                foreach (string word in words)
                {
                    if (word != "")
                    {
                        query = $"call alias.insertNewWordBookProxy('{word}','{nameWordbook}',{id})";
                        command = new MySqlCommand(query, Connection);
                        command.ExecuteNonQuery();
                    }
                }
                return "words add";
            }
            catch (Exception ex) { return ex.ToString(); }
        }

        public static List<string> getWordsFormWordbook(string wordbook)
        {
            List<string> words = new List<string>();
            query = $"call alias.getWordsFromWordbook('{wordbook}')";
            MySqlCommand command = new MySqlCommand(query, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                words.Add(reader[0].ToString());
            }
            reader.Close();
            return words;
        }
    }
}
