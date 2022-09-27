using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public class GrammarTest : Lesson
    {
        public string Title { get; set; }
        public string ExerciseText { get; set; }
        public string RightAnswer { get; set; }
        public string WrongAnswer1 { get; set; }
        public string WrongAnswer2 { get; set; }
        public static GrammarTest LoadTestFromDB(GrammarRule grammarRule)
        {
            GrammarTest grammarTest = new GrammarTest();
            sqlExpression = @"SELECT * FROM GrammarTests
                              WHERE GrammarRuleId=@GrammarRuleId";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter grammarRuleIdParam = new SqliteParameter("@GrammarRuleId", grammarRule.Id);
                command.Parameters.Add(grammarRuleIdParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            grammarTest.Id = reader.GetInt32(0);
                            grammarTest.Title = reader.GetString(2);
                            grammarTest.ExerciseText = reader.GetString(3);
                            grammarTest.RightAnswer = reader.GetString(4);
                            grammarTest.WrongAnswer1 = reader.GetString(5);
                            grammarTest.WrongAnswer2 = reader.GetString(6);
                            grammarTest.AudioPath = reader.GetString(7);
                            grammarTest.IsLearned = false;
                        }
                    }
                }
            }
            return grammarTest;
        }
    }
}
