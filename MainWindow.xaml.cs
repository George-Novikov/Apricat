using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
        public MainWindow()
        {
            InitializeComponent();

            if (User.CurrentUser is not null)
            {
                helloTextBlock.Text = "Привет, " + User.CurrentUser.UserName.ToString() + "!";
                viewModel = new ViewModel();
                DataContext = viewModel;
                PreloadProgress();
            }
            else
            {
                this.Close();
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }
        public async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            bool passed;

            if (grammarTestListBox.SelectedItem != null)
            {
                ListBoxItem listBoxItem = (ListBoxItem)grammarTestListBox.SelectedItem;
                passed = viewModel.CheckLesson(listBoxItem.Content.ToString());
                EllipsePainter(passed);
                grammarTestListBox.SelectedItem = null;
            }
            else
            {
                passed = viewModel.CheckLesson(inputTextBox.Text);
                EllipsePainter(passed);
            }

            if (passed)
            {
                ShowHeartsAnimation();
                nextButton.IsEnabled = false;
                await Task.Delay(625);
                ShowHeartsAnimation();
                nextButton.IsEnabled = true;
            }
            else
            {
                nextButton.IsEnabled = false;
                await Task.Delay(625);
                nextButton.IsEnabled = true;
            }
            
            if (viewModel.CheckAvailability())
            {
                viewModel.PrepareWorkplace();
            }
            else
            {
                //washingAnimation.Visibility = Visibility.Visible;
                nextButton.IsEnabled = false;
                MessageBox.Show("Все уроки пройдены. Отличная работа!");
            }

            inputTextBox.Visibility = Visibility.Collapsed;
            spaceTextBlock.Visibility = Visibility.Visible;
            inputTextBox.Text = "";
        }
        public void EllipsePainter(bool passed)
        {
            if (passed)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Colors.LightGreen);
                ellipse.Height = 15;
                ellipse.Width = 15;
                ellipse.Margin = new Thickness(2);
                answersIndicator.Children.Add(ellipse);
            }
            else
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Colors.IndianRed);
                ellipse.Height = 15;
                ellipse.Width = 15;
                ellipse.Margin = new Thickness(2);
                answersIndicator.Children.Add(ellipse);
            }
        }
        public void ShowHeartsAnimation()
        {
            if (mainAnimationImage.Visibility == Visibility.Visible)
            {
                mainAnimationImage.Visibility = Visibility.Collapsed;
                heartsAnimationImage.Visibility = Visibility.Visible;
            }
            else
            {
                mainAnimationImage.Visibility = Visibility.Visible;
                heartsAnimationImage.Visibility = Visibility.Collapsed;
            }
        }
        public void lessonButton_Click(object sender, RoutedEventArgs e)
        {
            workplaceGroupBox.Visibility = Visibility.Visible;
            progressGroupBox.Visibility = Visibility.Collapsed;

            answersIndicator.Children.Clear();

            spaceTextBlock.Visibility = Visibility.Visible;
            inputTextBox.Text = "";
            nextButton.IsEnabled = true;

            viewModel.Lessons.Clear();
            viewModel.wordsBuffer.Clear();

            viewModel.LoadLessonsFromDB(User.CurrentUser);
            viewModel.PrepareWorkplace();
        }
        public void repetitionButton_Click(object sender, RoutedEventArgs e)
        {
            workplaceGroupBox.Visibility = Visibility.Visible;
            progressGroupBox.Visibility = Visibility.Collapsed;

            answersIndicator.Children.Clear();

            spaceTextBlock.Visibility = Visibility.Visible;
            inputTextBox.Text = "";
            nextButton.IsEnabled = true;

            viewModel.Lessons.Clear();
            viewModel.wordsBuffer.Clear();

            viewModel.LoadRepetition(User.CurrentUser);
            viewModel.PrepareWorkplace();
        }
        public void PreloadProgress()
        {
            vocabularyTextBlock.Text = User.CurrentUser.CountLearnedWords().ToString();
            sentenceTextBlock.Text = User.CurrentUser.CountLearnedSentences().ToString();
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

            answersIndicator.Children.Clear();
        }
        public void space_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inputTextBox.Visibility = Visibility.Visible;
            spaceTextBlock.Visibility = Visibility.Collapsed;
        }
        public void playButton_Click(object sender, RoutedEventArgs e)
        {
            string meow = "meow.wav";
            string audioPath = "..\\audio\\" + meow;

            if (viewModel.AudioPath != null)
            {
                audioPath = "..\\audio\\" + viewModel.AudioPath;
            }

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
        public void mainAnimation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string audioPath = "..\\audio\\meow.wav";
            Uri audioUri = new Uri(audioPath, UriKind.RelativeOrAbsolute);
            StreamResourceInfo streamResourceInfo = Application.GetResourceStream(audioUri);
            SoundPlayer player = new SoundPlayer(streamResourceInfo.Stream);
            player.Play();
        }
    }
}
