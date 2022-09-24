using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Apricat
{
    public class Advice : DatabaseItem, INotifyPropertyChanged
    {
        public static Advice TodayAdvice { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public static Advice LoadAdviceFromDB()
        {
            Advice advice = new Advice();

            string dateNow = DateTime.Now.ToShortDateString(); //Here we get today's date
            string dateDigit = "";
            for (int i=0; i<2; i++)
            {
                dateDigit += dateNow[i];
            }
            int selectionId = int.Parse(dateDigit)/2; //And divide it by 2, to get an advice number

            sqlExpression = "SELECT * FROM Advices WHERE AdviceId=@selectionId";
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
