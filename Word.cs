using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public class Word : Lesson
    {
        public string Keyword { get; set; }
        public string Transcription { get; set; }
        public string Translation { get; set; }
        public static ObservableCollection<Word> LoadWordsFromDB(User user)
        {
            int wordsCount = user.DailyRate / 2 + user.DailyRate % 2;
            ObservableCollection<Word> wordList = new ObservableCollection<Word>();
            sqlExpression = @"SELECT * FROM Words
                              WHERE WordId NOT IN
                             (SELECT WordId FROM LearnedWords
                              WHERE UserId=@UserId)
                              AND Level=@UserLevel";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read() && wordList.Count < wordsCount)
                        {
                            Word derivedWord = new Word();
                            derivedWord.Id = reader.GetInt32(0);
                            derivedWord.Keyword = reader.GetString(1);
                            derivedWord.Transcription = reader.GetString(2);
                            derivedWord.Translation = reader.GetString(3);
                            derivedWord.Level = reader.GetString(4);
                            derivedWord.AudioPath = reader.GetString(5);
                            derivedWord.IsLearned = false;
                            wordList.Add(derivedWord);
                        }
                    }
                }
            }
            return wordList;
        }
        public static ObservableCollection<Word> LoadLearnedWords(User user)
        {
            int wordsCount = user.DailyRate / 2 + user.DailyRate % 2;
            ObservableCollection<Word> wordList = new ObservableCollection<Word>();

            //The difference is here - we choose only learned words
            sqlExpression = @"SELECT * FROM Words
                              WHERE WordId IN
                             (SELECT WordId FROM LearnedWords
                              WHERE UserId=@UserId)
                              AND Level=@UserLevel";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read() && wordList.Count < wordsCount)
                        {
                            Word derivedWord = new Word();
                            derivedWord.Id = reader.GetInt32(0);
                            derivedWord.Keyword = reader.GetString(1);
                            derivedWord.Transcription = reader.GetString(2);
                            derivedWord.Translation = reader.GetString(3);
                            derivedWord.Level = reader.GetString(4);
                            derivedWord.AudioPath = reader.GetString(5);
                            derivedWord.IsLearned = true;
                            wordList.Add(derivedWord);
                        }
                    }
                }
            }
            return wordList;
        }
    }
}
