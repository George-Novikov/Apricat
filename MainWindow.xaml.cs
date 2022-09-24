using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Printing;
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
using System.Windows.Resources;
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

            PreloadProgress();

            DataContext = viewModel;
        }
        private void settingsButton_GroupBoxCollapse(object sender, RoutedEventArgs e)
        {
            if (settingsGroupBox.Visibility == Visibility.Collapsed)
                settingsGroupBox.Visibility = Visibility.Visible;
            else settingsGroupBox.Visibility = Visibility.Collapsed;
        }
        public void nextButton_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.CheckIfCorrect(lesson);
            
            if (viewModel.CheckAvailability())
            {
                viewModel.IncrementLesson();
                viewModel.PrepareWorkplace();
            }
            else
            {
                MessageBox.Show("Good work!");
            }
        }
        public void lessonButton_Click(object sender, RoutedEventArgs e)
        {
            workplaceGroupBox.Visibility = Visibility.Visible;
            progressGroupBox.Visibility = Visibility.Collapsed;

            viewModel.LoadLessonsFromDB(User.CurrentUser);
            viewModel.PrepareWorkplace();
        }
        public void repetitionButton_Click()
        {
            workplaceGroupBox.Visibility = Visibility.Visible;
            progressGroupBox.Visibility = Visibility.Collapsed;
        }
        public void PreloadProgress()
        {
            vocabularyTextBlock.Text = User.CurrentUser.CountLearnedWords().ToString();
            rulesTextBlock.Text = User.CurrentUser.CountLearnedGrammar().ToString();
            levelTextBlock.Text = User.CurrentUser.Level;

            Advice.TodayAdvice = Advice.LoadAdviceFromDB();
            adviceHeader.Text = "Совет дня: "+Advice.TodayAdvice.Title;
            adviceTextBlock.Text = Advice.TodayAdvice.Content;
        }
        public void progressButton_Click(object sender, RoutedEventArgs e)
        {
            workplaceGroupBox.Visibility = Visibility.Collapsed;
            progressGroupBox.Visibility = Visibility.Visible;
            PreloadProgress();
        }
        public void space_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputTextBox.Visibility = Visibility.Visible;
            spaceTextBlock.Visibility = Visibility.Collapsed;
        }
        public void playButton_Click(object sender, RoutedEventArgs e)
        {
            string meow = "meow.wav";
            string audioPath = "..\\audio\\" + meow; //+viewModel.AudioPath

            /*if (viewModel.AudioPath != null)
            {
                audioPath = viewModel.AudioPath;
            }*/
            Uri audioUri = new Uri(audioPath, UriKind.RelativeOrAbsolute);

            try
            {
                StreamResourceInfo streamResourceInfo = Application.GetResourceStream(audioUri);
                SoundPlayer player = new SoundPlayer(streamResourceInfo.Stream);
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
