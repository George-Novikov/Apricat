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
        }
        void registerButton_Click(object sender, RoutedEventArgs e)
        {

        }
        void StartUpClosing_LoadMainWindow(object sender, CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
