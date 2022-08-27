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
        static string connectionString = "Data Source=lessons.db";
        public MainWindow()
        {
            InitializeComponent();
            /*
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
            User george = new User("George", "Upper-Intermediate");
            Word word = new Word();
            word.LoadLessonsFromDB(george);
            workspaceTextBlock.Text = word.ToString();
        }
        void settingsButton_GroupBoxCollapse(object sender, RoutedEventArgs e)
        {
            if (settingsGroupBox.Visibility == Visibility.Collapsed)
                settingsGroupBox.Visibility = Visibility.Visible;
            else settingsGroupBox.Visibility = Visibility.Collapsed;
        }
        private class User
        {
            public object Id { get; set; }
            public string UserName { get; set; }
            public string Level { get; set; } = "Beginner";
            public int Vocabulary = 0;
            public int KnownGrammar = 0;
            public User(string username)
            {
                UserName = username;
                string sqlExpression = @"INSERT INTO Users (UserName, Level)
                                         VALUES (@UserName, @Level);
                                         SELECT last_insert_rowid()";
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    SqliteParameter usernameParam = new SqliteParameter("@UserName", this.UserName);
                    command.Parameters.Add(usernameParam);
                    SqliteParameter levelParam = new SqliteParameter("@Level", this.Level);
                    command.Parameters.Add(levelParam);
                    Id = command.ExecuteScalar();                    
                }
            }
            public User(string username, string level) : this(username)
            {
                Level = level;
            }
            public void SetLevel()
            {
                /*
                int[] learnedWords = new int[0];
                int[] learnedRules = new int[0];
                Vocabulary = learnedWords.Count();
                KnownGrammar = learnedRules.Count();

                if (Vocabulary >= 0 && KnownGrammar >= 0)
                    this.Level = "Beginner";
                else if (Vocabulary > 500 && KnownGrammar > 10)
                    this.Level = "Elementary";
                else if (Vocabulary > 1000 && KnownGrammar > 20)
                    this.Level = "Intermediate";
                else if (Vocabulary > 2000 && KnownGrammar > 30)
                    this.Level = "Upper-Intermediate";
                else if (Vocabulary > 5000 && KnownGrammar > 40)
                    this.Level = "Advanced";
                else if (Vocabulary > 10000 && KnownGrammar > 50)
                    this.Level = "Proficiency";
                string sqlExpression = "UPDATE Users SET Level=@Level WHERE UserId=@UserId";
                */
            }
        }
        private class Lesson
        {
            public int Id { get; set; }
            public bool Learned { get; set; } = false;
            public string Level { get; set; }
            public string AudioPath { get; set; }
            public virtual void MarkLearned()
            {
                this.Learned = true;
            }
        }
        private class Word : Lesson
        {
            public string Keyword { get; set; }
            public string Transcription { get; set; }
            public string Translation { get; set; }

            public void LoadLessonsFromDB(User user)
            {
                string sqlExpression = @"SELECT WordId FROM LearnedWords
                                         WHERE UserId=@UserId";
                List<int> learnedWordsId = new List<int>();
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
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                learnedWordsId.Add(id);
                            }
                        }
                    }
                }
                foreach (int id in learnedWordsId)
                {
                    sqlExpression = "SELECT";
                }    
            }
            public override string ToString()
            {
                return $"{Id} {Keyword} {Transcription} {Translation} {Level} {AudioPath}";
            }
            public override void MarkLearned()
            {
                this.Learned = true;
                string sqlExpression = @"INSERT INTO LearnedWords (WordId, UserId)
                                         VALUES (@WordId, @UserId)";
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    SqliteParameter wordIdParam = new SqliteParameter("@WordId", this.Id);
                    command.Parameters.Add(wordIdParam);
                    //SqliteParameter userIdParam = new SqliteParameter("@UserId", int id);
                    command.ExecuteNonQuery();
                }
            }
        }
        private class Sentence : Lesson
        {
            public override void MarkLearned()
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
        private class GrammarRule : Lesson
        {
            public void CallTest()
            {

            }
        }
        private class Advice
        {

        }
        private class GrammarTest
        {
            public int Id { get; set; }
            public string Title { get; set; }


        }
        
    }
}
