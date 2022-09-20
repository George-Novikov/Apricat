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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Apricat;


namespace Apricat
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Window
    {
        public StartUp()
        {
            InitializeComponent();
            
            List<User> users = User.GetAllUsersFromDB();
            GenerateLoginButtons(users);
        }
        void GenerateLoginButtons(List<User> users)
        {
            if (users.Count == 0)
            {
                loginGroupBox.Visibility = Visibility.Collapsed;
                registerGroupBox.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (User user in users)
                {
                    Button userButton = new Button();
                    TextBlock userButtonText = new TextBlock();
                    userButtonText.TextAlignment = TextAlignment.Center;

                    string userLevel = user.Level;
                    if (userLevel == "Upper-Intermediate")
                    {
                        userLevel = "Upper-\nIntermediate";
                    }

                    userButtonText.Text = $"{user.UserName}\n({userLevel})";
                    userButton.Content = userButtonText;

                    userButton.Height = 95;
                    userButton.Width = 95;
                    userButton.Background = new SolidColorBrush(Color.FromRgb(255, 253, 250));
                    userButton.Foreground = new SolidColorBrush(Colors.SaddleBrown);
                    userButton.BorderBrush = new SolidColorBrush(Colors.SaddleBrown);
                    userButton.HorizontalContentAlignment = HorizontalAlignment.Center;
                    userButton.HorizontalAlignment = HorizontalAlignment.Center;
                    userButton.Margin = new Thickness(10);

                    Style borderStyle = new Style { TargetType = typeof(Border) };
                    borderStyle.Setters.Add(new Setter { Property = Border.CornerRadiusProperty, Value = new CornerRadius(15) });
                    userButton.Resources.Add(borderStyle.TargetType, borderStyle);

                    userButton.Name = "_" + user.Id.ToString();
                    userButton.Click += LogInButton_Click;
                    userChoiceList.Children.Add(userButton);
                }
            }
        }
        void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Button bufferButton = (Button)sender;
            int userId = int.Parse(bufferButton.Name.Substring(bufferButton.Name.LastIndexOf("_") + 1));

            User.CurrentUser = User.GetUserFromDBById(userId);
            this.Close();
        }
        void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = registerTextBox.Text;
            string userLevel = "Beginner";
            int dailyRate = (int)dailyRateSlider.Value;

            if (beginnerButton.IsChecked == true)
            {
                userLevel = "Beginner";
            } else if (elementaryButton.IsChecked == true)
            {
                userLevel = "Elementary";
            } else if (intermediateButton.IsChecked == true)
            {
                userLevel = "Intermediate";
            } else if (upperIntermediateButton.IsChecked == true)
            {
                userLevel = "Upper-Intermediate";
            } else if (advancedButton.IsChecked == true)
            {
                userLevel = "Advanced";
            }

            User newUser = new User(userName, userLevel, dailyRate);
            User.CurrentUser = newUser;
            this.Close();
        }
        void StartUpClosing_LoadMainWindow(object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (User.CurrentUser != null)
            {
                mainWindow.Show();
            }            
        }
        void ToggleButtons_UncheckOnClick(object sender, RoutedEventArgs e)
        {
            ToggleButton senderButton = (ToggleButton)sender;
            List<object> buttons = new List<object>();
            buttons.Add(beginnerButton);
            buttons.Add(elementaryButton);
            buttons.Add(intermediateButton);
            buttons.Add(upperIntermediateButton);
            buttons.Add(advancedButton);
            foreach (ToggleButton button in buttons)
            {
                if (button.Name != senderButton.Name)
                {
                    button.IsChecked = false;
                }
            }
        }
    }
}
