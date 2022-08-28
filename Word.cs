using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    internal class Word : Lesson
    {
        public string Keyword { get; set; }
        public string Transcription { get; set; }
        public string Translation { get; set; }

        public void LoadLessonsFromDB(User user)
        {
            List<Word> wordList = new List<Word>();
            string sqlExpression = @"SELECT * FROM Words
                                         WHERE WordId NOT IN
                                        (SELECT WordId FROM LearnedWords
                                         WHERE UserId=@UserId)";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read() && wordList.Count < user.DailyRate)
                        {
                            Word derivedWord = new Word();
                            derivedWord.Id = reader.GetInt32(0);
                            derivedWord.Keyword = reader.GetString(1);
                            derivedWord.Transcription = reader.GetString(2);
                            derivedWord.Translation = reader.GetString(3);
                            derivedWord.Level = reader.GetString(4);
                            derivedWord.AudioPath = reader.GetString(5);
                            wordList.Add(derivedWord);
                        }
                    }
                }
            }
        }
        public override void MarkLearned(User user)
        {
            this.Learned = true;
            string sqlExpression = @"INSERT INTO LearnedWords (WordId, UserId)
                                     VALUES (@WordId, @UserId)";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter wordIdParam = new SqliteParameter("@WordId", this.Id);
                command.Parameters.Add(wordIdParam);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", user.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
