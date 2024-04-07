using PL.Engineer;
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


        public TaskInListWindow(bool f=false)
        {
            InitializeComponent();
            if(!f)
            TaskList = s_bl?.Task.ReadAll()!.OrderBy(t=>t.id)!;
            else
                TaskList = s_bl?.Task.ReadAll(t=>t.engineer==null)!;
        }

        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskInListWindow));
            
        public BO.Status status { get; set; } = BO.Status.None;

        /// <summary>
        /// Displaying the tasks according to the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectStatus(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (status == BO.Status.None) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.status == status)!;
        }

        /// <summary>
        /// Button to open the window for adding a new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickOpenTaskWindowForCreate(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
            TaskList = s_bl?.Task.ReadAll()!.OrderBy(t => t.id)!;
        }

        /// <summary>
        /// Button to open the task update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickOpenTaskWindowForUptade(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? en = (sender as ListView)?.SelectedItem as BO.TaskInList;
            new TaskWindow(en!.id).ShowDialog();
            TaskList = s_bl?.Task.ReadAll()!.OrderBy(t => t.id)!;
        }

        /// <summary>
        /// Filter tasks by ID number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskList = s_bl.Task.GetTasksGroupedByTaskIdSafe();
        }
    }
}
