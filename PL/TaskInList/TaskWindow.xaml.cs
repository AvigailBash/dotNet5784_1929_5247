using PL.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskWindow(int Id = 0, bool arriveFromEngineer = false)
        {
            InitializeComponent();
            try
            {
                if (Id != 0)
                    Dependencies = s_bl?.Task.findDependencies(s_bl?.Task.Read(Id)!)!;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (Id == 0)
            {
                Task = new BO.Task();

            }
            else
            {
                try
                {
                    Task = s_bl.Task.Read(Id)!;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
        }




        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));




        public BO.EngineerInTask EngineerInTask
        {
            get { return (BO.EngineerInTask)GetValue(EngineerInTaskProperty); }
            set { SetValue(EngineerInTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing  store for EngineerInTask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerInTaskProperty =
            DependencyProperty.Register("EngineerInTask", typeof(BO.EngineerInTask), typeof(TaskWindow), new PropertyMetadata(null));




        public IEnumerable<BO.TaskInList> Dependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(dependenciesProperty); }
            set { SetValue(dependenciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dependencies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty dependenciesProperty =
            DependencyProperty.Register("Dependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));




        private void ClickForAddDependencies(object sender, RoutedEventArgs e)
        {
            new AddDependenciesWIindow(Task.id).ShowDialog();
        }

        private void ClickForViewDependencies(object sender, RoutedEventArgs e)
        {
            new DependenciesListWindow(Task.id).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            try
            {

                if (button is { Content: "Add" })
                {
                    
                    s_bl.Task.Create(Task);
                    MessageBox.Show("success");
                    this.Close();
                }
                else
                {
                    s_bl.Task.Update(Task);
                    if (Task.completeDate != null)
                    {
                        this.Close();
                        MessageBoxResult result = MessageBox.Show("Do you want to start a new task", "newTask", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            new TaskInListWindow(true).ShowDialog();
                          

                        }
                    }
                    else
                    {
                        MessageBox.Show("success");
                        this.Close();

                    }

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); this.Close(); };

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }









    }
}

