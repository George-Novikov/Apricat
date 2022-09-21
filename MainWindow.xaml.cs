using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
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
        private ViewModel viewModel;
        public static string connectionString = "Data Source=lessons.db";
        public MainWindow()
        {
            InitializeComponent();
            if (User.CurrentUser is not null)
            {
                helloTextBlock.Text = "Привет " + User.CurrentUser.UserName.ToString() + "!";
            } else this.Close();
            viewModel = new ViewModel();
            DataContext = viewModel; ;
        }
        private void settingsButton_GroupBoxCollapse(object sender, RoutedEventArgs e)
        {
            if (settingsGroupBox.Visibility == Visibility.Collapsed)
                settingsGroupBox.Visibility = Visibility.Visible;
            else settingsGroupBox.Visibility = Visibility.Collapsed;
        }
        public void nextLessonButton_Click(object sender, RoutedEventArgs e)
        {
            //CheckIfCorrect(lesson);
        }
        public void PrepareWorkplace(ObservableCollection<Lesson> lessons)
        {
            int currentSession = lessons.Count;
            if (currentSession > 0)
            {
                foreach (Lesson lesson in lessons)
                {
                    if (lesson.GetType() == typeof(Sentence))
                    {
                        

                    }
                    else if (lesson.GetType() == typeof(GrammarRule))
                    {

                    }
                    else if (lesson.GetType() == typeof(GrammarTest))
                    {

                    }
                    else
                    {
                        StudyWord(lesson);
                        --currentSession;
                    }
                }
            }
            else
            {
                //nextLessonButton.IsActive = False;
            }
        }
        public void playButton_Click(object sender, RoutedEventArgs e)
        {
            string audioPath = viewModel.AudioPath;
            using (SoundPlayer player = new SoundPlayer(audioPath))
            {
                try
                {
                    player.Load();
                    player.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void StudyWord(Lesson lesson)
        {
            Word word = (Word)lesson;
            lessonHeader.Text = word.Keyword;
        }
        public void StudySentence()
        {
            lessonHeader.Text = "Заполните недостающее слово";
        }
        public void StudyGrammar()
        {

        }
        public void TakeATest()
        {

        }
    }
}
