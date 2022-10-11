using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public class Sentence : Lesson
    {
        public string MissingWord { get; set; }
        public string SentenceLeftPart { get; set; }
        public string SentenceRightPart { get; set; }
        public string Translation { get; set; }
        public static Sentence LoadSentenceByWord(User user, Word word)
        {
            Sentence sentence = new Sentence();
            int sentenceCount = user.DailyRate / 2;
            sqlExpression = @"SELECT * FROM Sentences
                              WHERE MissingWord=@WordKeyword";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter wordKeywordParam = new SqliteParameter("@WordKeyword", word.Keyword);
                command.Parameters.Add(wordKeywordParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sentence.Id = reader.GetInt32(0);
                            sentence.MissingWord = reader.GetString(1);
                            sentence.SentenceLeftPart = reader.GetString(2);
                            sentence.SentenceRightPart = reader.GetString(3);
                            sentence.Translation = reader.GetString(4);
                            sentence.Level = reader.GetString(5);
                            sentence.AudioPath = sentence.MissingWord + " sentence.wav";
                            sentence.IsLearned = false;
                        }
                    }
                }
            }
            return sentence;
        }
        public static ObservableCollection<Sentence> LoadSentencesFromDB(User user)
        {
            int sentenceCount = user.DailyRate / 2;
            ObservableCollection<Sentence> sentenceList = new ObservableCollection<Sentence>();
            sqlExpression = @"SELECT * FROM Sentences
                              WHERE SentenceId NOT IN
                             (SELECT SentenceId FROM LearnedSentences
                              WHERE UserId=@UserId)
                              AND Level=@UserLevel
                              ORDER BY RANDOM() LIMIT @Limit";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("@UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
                SqliteParameter limitParam = new SqliteParameter("@Limit", sentenceCount);
                command.Parameters.Add(limitParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read() && sentenceList.Count < sentenceCount)
                        {
                            Sentence derivedSentence = new Sentence();
                            derivedSentence.Id = reader.GetInt32(0);
                            derivedSentence.MissingWord = reader.GetString(1);
                            derivedSentence.SentenceLeftPart = reader.GetString(2);
                            derivedSentence.SentenceRightPart = reader.GetString(3);
                            derivedSentence.Translation = reader.GetString(4);
                            derivedSentence.Level = reader.GetString(5);
                            derivedSentence.AudioPath = derivedSentence.MissingWord + " sentence.wav";
                            derivedSentence.IsLearned = false;
                            sentenceList.Add(derivedSentence);
                        }
                    }
                }
            }
            return sentenceList;
        }
        public static ObservableCollection<Sentence> LoadLearnedSentences(User user)
        {
            int sentenceCount = user.DailyRate / 2;
            ObservableCollection<Sentence> sentenceList = new ObservableCollection<Sentence>();

            //The difference is here - we choose only learned sentences
            sqlExpression = @"SELECT * FROM Sentences
                              WHERE SentenceId IN
                             (SELECT SentenceId FROM LearnedSentences
                              WHERE UserId=@UserId)
                              ORDER BY RANDOM() LIMIT @Limit";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter limitParam = new SqliteParameter("@Limit", sentenceCount);
                command.Parameters.Add(limitParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read() && sentenceList.Count < sentenceCount)
                        {
                            Sentence derivedSentence = new Sentence();
                            derivedSentence.Id = reader.GetInt32(0);
                            derivedSentence.MissingWord = reader.GetString(1);
                            derivedSentence.SentenceLeftPart = reader.GetString(2);
                            derivedSentence.SentenceRightPart = reader.GetString(3);
                            derivedSentence.Translation = reader.GetString(4);
                            derivedSentence.Level = reader.GetString(5);
                            derivedSentence.AudioPath = derivedSentence.MissingWord + " sentence.wav";
                            derivedSentence.IsLearned = true;
                            sentenceList.Add(derivedSentence);
                        }
                    }
                }
            }
            return sentenceList;
        }
    }
}
