using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public class User : DatabaseItem
    {
        public static User CurrentUser;
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

            sqlExpression = @"INSERT INTO Users
                             (UserName, Level, DailyRate, Vocabulary, GrammarKnowledge)
                              VALUES (@UserName, @Level, @DailyRate, 0, 0);
                              SELECT last_insert_rowid()";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter usernameParam = new SqliteParameter("@UserName", this.UserName);
                command.Parameters.Add(usernameParam);
                SqliteParameter levelParam = new SqliteParameter("@Level", this.Level);
                command.Parameters.Add(levelParam);
                SqliteParameter dailyRateParam = new SqliteParameter("DailyRate", this.DailyRate);
                command.Parameters.Add(dailyRateParam);
                Id = command.ExecuteScalar();
            }
        }
        public void GetLevel()
        {

        }
        public void UpdateLevel()
        {

        }
        public void UpdateDailyRate()
        {

        }
        public void ShowLearnedWords()
        {

        }
        public void ShowLearnedSentences()
        {

        }
        public void ShowLearnedGrammarRules()
        {

        }
        internal static List<User>GetAllUsersFromDB()
        {
            List<User> users = new List<User>();
            sqlExpression = "SELECT * FROM Users";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.GetInt32(0);
                            user.UserName = reader.GetString(1);
                            user.Level = reader.GetString(2);
                            user.DailyRate = reader.GetInt32(3);
                            user.Vocabulary = reader.GetInt32(4);
                            user.GrammarKnowledge = reader.GetInt32(5);
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        internal static User GetUserFromDBById(int id)
        {
            User loginUser = new User();
            sqlExpression = @"SELECT * FROM Users
                              WHERE UserId=@UserId";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", id);
                command.Parameters.Add(userIdParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            loginUser.Id = reader.GetInt32(0);
                            loginUser.UserName = reader.GetString(1);
                            loginUser.Level = reader.GetString(2);
                            loginUser.DailyRate = reader.GetInt32(3);
                            loginUser.Vocabulary = reader.GetInt32(4);
                            loginUser.GrammarKnowledge = reader.GetInt32(5);
                        }
                    }
                }
            }
            return loginUser;
        }
    }
}
