using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Apricat
{
    public class GrammarRule : Lesson
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public static GrammarRule LoadRuleFromDB(User user)
        {
            sqlExpression = @"SELECT * FROM GrammarRules
                              WHERE GrammarRuleId NOT IN
                             (SELECT GrammarRuleId FROM LearnedGrammar
                              WHERE UserId=@UserId)
                              AND Level=@UserLevel";
            GrammarRule rule = new GrammarRule();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("@UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rule.Id = reader.GetInt32(0);
                            rule.Title = reader.GetString(1);
                            rule.Content = reader.GetString(2);
                            rule.Level = reader.GetString(3);
                            rule.AudioPath = reader.GetString(4);
                        }
                    }
                }
            }
            return rule;
        }
        public static GrammarRule LoadLearnedRule(User user)
        {
            //The difference is here - we choose only learned rules
            sqlExpression = @"SELECT * FROM GrammarRules
                              WHERE GrammarRuleId IN
                             (SELECT GrammarRuleId FROM LearnedGrammar
                              WHERE UserId=@UserId)
                              AND Level=@UserLevel";
            GrammarRule rule = new GrammarRule();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", user.Id);
                command.Parameters.Add(userIdParam);
                SqliteParameter userLevelParam = new SqliteParameter("@UserLevel", user.Level);
                command.Parameters.Add(userLevelParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rule.Id = reader.GetInt32(0);
                            rule.Title = reader.GetString(1);
                            rule.Content = reader.GetString(2);
                            rule.Level = reader.GetString(3);
                            rule.AudioPath = reader.GetString(4);
                        }
                    }
                }
            }
            return rule;
        }
        public GrammarTest CallTest(GrammarRule rule)
        {
            sqlExpression = @"SELECT * FROM GrammarTests
                              WHERE GrammarRuleId=@GrammarRuleId";
            GrammarTest grammarTest = new GrammarTest();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter grammarRuleIdParam = new SqliteParameter("@GrammarRuleId", rule.Id);
                command.Parameters.Add(grammarRuleIdParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        grammarTest.Id = reader.GetInt32(0);
                        grammarTest.Title = reader.GetString(1);
                        grammarTest.ExerciseText = reader.GetString(1);
                        grammarTest.RightAnswer = reader.GetString(2);
                        grammarTest.WrongAnswer1 = reader.GetString(3);
                        grammarTest.WrongAnswer2 = reader.GetString(4);
                        grammarTest.AudioPath = reader.GetString(5);
                    }
                }
            }
            return grammarTest;
        }
    }
}
