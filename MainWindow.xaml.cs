using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace Apricat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            string connectionString = "Data Source=lessons.db";
            string sqlExpression = @"";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Table was successfully created");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            */
        }
        void settingsButton_GroupBoxCollapse(object sender, RoutedEventArgs e)
        {
            if (settingsGroupBox.Visibility == Visibility.Collapsed)
                settingsGroupBox.Visibility = Visibility.Visible;
            else settingsGroupBox.Visibility = Visibility.Collapsed;
        }
        private abstract class Lesson
        {
            public string Id { get; set; }
            public string KeyWord { get; set; }
            public string Level { get; set; }

            public void ShowWord() { }
            public void ControlWord() { }
            public void MarkLearned() { }
        }
        private class WordLesson : Lesson
        {
            
        }
        private class SentenceLesson : Lesson
        {

        }
        private class GrammarLessson : Lesson
        {

        }
        private class ExcerciseLesson : Lesson
        {

        }
        private class AdviceLesson : Lesson
        {

        }
    }
}
