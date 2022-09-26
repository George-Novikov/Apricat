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
        public static ObservableCollection<Sentence> LoadSentencesFromDB(User user)
        {
            int sentenceCount = user.DailyRate / 2;
            ObservableCollection<Sentence> sentenceList = new ObservableCollection<Sentence>();
            sqlExpression = @"SELECT * FROM Sentences
                              WHERE SentenceId NOT IN
                             (SELECT SentenceId FROM LearnedSentences
                              WHERE UserId=@UserId)
                              AND Level=@UserLevel";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("@UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
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
                            derivedSentence.AudioPath = reader.GetString(6);
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
                              AND Level=@UserLevel";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter(@"UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("@UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
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
                            derivedSentence.AudioPath = reader.GetString(6);
                            sentenceList.Add(derivedSentence);
                        }
                    }
                }
            }
            return sentenceList;
        }
    }
}
