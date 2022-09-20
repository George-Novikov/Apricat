using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public class Sentence : Lesson
    {
        internal string MissingWord { get; set; }
        internal string IncompleteSentence { get; set; }
        internal string Translation { get; set; }
        internal static List<Sentence> LoadSentencesFromDB(User user)
        {
            int sentenceCount = user.DailyRate / 2;
            List<Sentence> sentenceList = new List<Sentence>();
            string sqlExpression = @"SELECT * FROM Sentences
                                     WHERE SentenceId NOT IN
                                    (SELECT SentenceId FROM LearnedSentences
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
                        while (reader.Read() && sentenceList.Count < sentenceCount)
                        {
                            Sentence derivedSentence = new Sentence();
                            derivedSentence.Id = reader.GetInt32(0);
                            derivedSentence.MissingWord = reader.GetString(1);
                            derivedSentence.IncompleteSentence = reader.GetString(2);
                            derivedSentence.Translation = reader.GetString(3);
                            derivedSentence.Level = reader.GetString(4);
                            derivedSentence.AudioPath = reader.GetString(5);
                            sentenceList.Add(derivedSentence);
                        }
                    }
                }
            }
            return sentenceList;
        }
    }
}
