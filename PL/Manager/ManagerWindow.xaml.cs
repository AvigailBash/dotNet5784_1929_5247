﻿using PL.Engineer;
using PL.TaskInList;
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

namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void ClickForEngineerList(object sender, RoutedEventArgs e)
        {

            new EngineerListWindow().Show();
        }

        private void ClickForTaskInList(object sender, RoutedEventArgs e)
        {
            new TaskInListWindow().Show();
        }

        private void ClickForAddTask(object sender, RoutedEventArgs e)
        {
            new TaskWindow().Show();
        }

        private void ClickForReset(object sender, RoutedEventArgs e)
        {
            s_bl.Help.reset();
            MessageBox.Show("The reset was completed successfully");
        }
    }
}
