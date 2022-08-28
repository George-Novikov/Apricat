using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    internal class User : DatabaseItem
    {
        public object Id { get; set; }
        public string UserName { get; set; }
        public string Level { get; set; } = "Beginner";
        public int DailyRate { get; set; } = 10; //Number of words user wants to learn every day
        public int Vocabulary = 0;
        public int GrammarKnowledge = 0;
        public User() { }
        public User(string username, string level, int dailyrate)
        {
            UserName = username;
            Level = level;
            DailyRate = dailyrate;
            string sqlExpression = @"INSERT INTO Users (UserName, Level)
                                         VALUES (@UserName, @Level);
                                         SELECT last_insert_rowid()";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter usernameParam = new SqliteParameter("@UserName", this.UserName);
                command.Parameters.Add(usernameParam);
                SqliteParameter levelParam = new SqliteParameter("@Level", this.Level);
                command.Parameters.Add(levelParam);
                Id = command.ExecuteScalar();
            }
        }
        public void SetLevel()
        {

        }
        public void SetDailyRate()
        {

        }
        public void ShowVocabulary()
        {

        }
        public void ShowGrammarKnowledge()
        {

        }
        static bool CheckUsers()
        {
            string sqlExpression = "SELECT * FROM Users";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
        static void CreateUser()
        {

        }
        static void LogIn()
        {
            User currentUser = new User();

        }
    }
}
