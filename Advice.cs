using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public class Advice : DatabaseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public Advice LoadAdviceFromDB()
        {
            Advice advice = new Advice();
            Random random = new Random();
            int selectionId = random.Next(1, 20);
            sqlExpression = "SELECT * FROM Advices WHERE Id=@selectionId";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter selectedIdParam = new SqliteParameter("@selectionId", selectionId);
                command.Parameters.Add(selectedIdParam);
                using (SqliteDataReader reader = command.ExecuteReader())
                { 
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            advice.Id = reader.GetInt32(0);
                            advice.Title = reader.GetString(1);
                            advice.Content = reader.GetString(2);
                            advice.ImagePath = reader.GetString(3);
                        }
                    }
                }
            }
            return advice;
        }
    }
}
