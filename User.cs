using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Apricat
{
    public class User : DatabaseItem, INotifyPropertyChanged
    {
        public static User CurrentUser { get; set; }
        public object Id { get; set; }
        public string UserName { get; set; }
        public string Level { get; set; } = "Beginner";
        public int DailyRate { get; set; } = 10; //Number of words user wants to learn every day
        public User() { }
        public User(string username, string level, int dailyrate)
        {
            UserName = username;
            Level = level;
            DailyRate = dailyrate;

            sqlExpression = @"INSERT INTO Users
                             (UserName, Level, DailyRate)
                              VALUES (@UserName, @Level, @DailyRate);
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
        public static User GetUserFromDBById(int id)
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
                        }
                    }
                }
            }
            return loginUser;
        }
        public static List<User>GetAllUsersFromDB()
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
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        public void UpdateUserLevel()
        {

        }
        public void UpdateUserDailyRate()
        {

        }
        public object CountLearnedWords()
        {
            sqlExpression = @"SELECT COUNT(LearnedWordId)
                               FROM LearnedWords";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                object wordsCount = command.ExecuteScalar();
                return wordsCount;
            }
        }
        public object CountLearnedSentences()
        {
            sqlExpression = @"SELECT COUNT(LearnedSentenceId)
                               FROM LearnedSentences";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                object sentenceCount = command.ExecuteScalar();
                return sentenceCount;
            }
        }
        public object CountLearnedGrammar()
        {
            sqlExpression = @"SELECT COUNT(LearnedGrammarId)
                               FROM LearnedGrammar";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                object grammarCount = command.ExecuteScalar();
                return grammarCount;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
