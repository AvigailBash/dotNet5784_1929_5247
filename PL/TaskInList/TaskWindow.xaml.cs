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

namespace PL.TaskInList
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            try
            {
                dependencies = s_bl?.Task.findDependencies(s_bl?.Task.Read(Id))!;
            }
            catch (Exception ex)
            {

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
                catch (Exception ex) { Console.WriteLine(ex); }

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







        public IEnumerable<BO.TaskInList> dependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(dependenciesProperty); }
            set { SetValue(dependenciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for dependencies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty dependenciesProperty =
            DependencyProperty.Register("dependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));



    }
}
