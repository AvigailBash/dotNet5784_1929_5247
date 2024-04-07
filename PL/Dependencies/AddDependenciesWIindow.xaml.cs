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

namespace PL.Dependencies
{
    /// <summary>
    /// Interaction logic for AddDependenciesWIindow.xaml
    /// </summary>
    public partial class AddDependenciesWIindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public AddDependenciesWIindow(int id)
        {
            InitializeComponent();
            TaskList= s_bl?.Task.ReadAll(t=>t.id!=id)!.OrderBy(t=>t.id);
            task = s_bl?.Task.Read(id)!;
           
        }

        public BO.Task task
        {
            get { return (BO.Task)GetValue(taskProperty); }
            set { SetValue(taskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty taskProperty =
            DependencyProperty.Register("task", typeof(BO.Task), typeof(AddDependenciesWIindow), new PropertyMetadata(null));



        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(AddDependenciesWIindow), new PropertyMetadata(null));

        /// <summary>
        /// Adding the dependency to the task's dependency list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForAddDependencies(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// Displays the list of all dependencies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.TaskInList? en = (sender as ListBox)?.SelectedItem as BO.TaskInList;
            if (en != null) { task.dependencies!.Add(en);s_bl.Task.Update(task); }
           
        }
    }
}
