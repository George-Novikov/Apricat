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
                              AND Level=@UserLevel
                              ORDER BY RANDOM() LIMIT 1";
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
                            rule.IsLearned = false;
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
                              ORDER BY RANDOM() LIMIT 1";
            GrammarRule rule = new GrammarRule();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", user.Id);
                command.Parameters.Add(userIdParam);
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
                            rule.IsLearned = true;
                        }
                    }
                }
            }
            return rule;
        }
    }
}
