﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
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
        public static List<Sentence> LoadSentencesFromDB(User user)
        {
            int sentenceCount = user.DailyRate / 2;
            List<Sentence> sentenceList = new List<Sentence>();
            sqlExpression = @"SELECT * FROM Sentences
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
