﻿using System;
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
        public static string connectionString = "Data Source=lessons.db";
        public MainWindow()
        {
            InitializeComponent();
            if (User.currentUser is not null)
            {
                lessonHeader.Text = "Привет " + User.currentUser.UserName.ToString() + "!";
            }
            
        }

        void settingsButton_GroupBoxCollapse(object sender, RoutedEventArgs e)
        {
            if (settingsGroupBox.Visibility == Visibility.Collapsed)
                settingsGroupBox.Visibility = Visibility.Visible;
            else settingsGroupBox.Visibility = Visibility.Collapsed;
        }
    }
}
