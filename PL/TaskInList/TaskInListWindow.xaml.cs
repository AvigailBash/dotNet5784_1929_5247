﻿using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace PL.TaskInList
{
    /// <summary>
    /// Interaction logic for TaskInListWindow.xaml
    /// </summary>
    public partial class TaskInListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        public TaskInListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }



        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskInListWindow));
        public BO.Status Status { get; set; } = BO.Status.None;

        private void SelectStatus(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Status == BO.Status.None) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.status == Status)!;
        }

        private void clickOpenTaskWindowForCreate(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
            TaskList = s_bl?.Task.ReadAll()!;
        }

        private void clickOpenTaskWindowForUptade(object sender, MouseButtonEventArgs e)
        {
            BO.Task? t = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(t!.id).ShowDialog();
            TaskList = s_bl?.Task.ReadAll()!;
        }
    }
}
