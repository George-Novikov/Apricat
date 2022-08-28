using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    internal class Sentence : Lesson
    {
        public override void MarkLearned(User user)
        {
            this.Learned = true;
            string sqlExpression = @"INSERT INTO LearnedSentences (SentenceId, UserId)
                                     VALUES (@SentenceId, @UserId)";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
