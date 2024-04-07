using PL.Engineer;
using PL.Gantt;
using PL.Schedule;
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

        /// <summary>
        /// Button to open the list of engineers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForEngineerList(object sender, RoutedEventArgs e)
        {

            new EngineerListWindow().Show();
        }

        /// <summary>
        /// Button to open the list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForTaskInList(object sender, RoutedEventArgs e)
        {
            new TaskInListWindow().Show();
        }

        /// <summary>
        /// Button to open adding a new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForAddTask(object sender, RoutedEventArgs e)
        {
            new TaskWindow().Show();
        }

        /// <summary>
        /// Data reset button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForReset(object sender, RoutedEventArgs e)
        {
            s_bl.Help.reset();
            MessageBox.Show("The reset was completed successfully");
        }

        /// <summary>
        /// A button to select start and end dates for the project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForSetStartAndEndDates(object sender, RoutedEventArgs e)
        {
            new SetStartAndEndProject().ShowDialog();
        }

        /// <summary>
        /// A button to set an automatic schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForAutomaticScheduale(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.Clock.GetStartOfProject() == null)
                    MessageBox.Show("Set dates for the project before using automatic schedule");
                else
                {
                    s_bl.Help.AutomaticScheduale();
                    MessageBox.Show("success");
                }
               
            }
            catch(Exception ex)
            {  MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Button to open the Gantt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForOpenGant(object sender, RoutedEventArgs e)
        {
            IEnumerable<BO.TaskInList>? taskList = s_bl.Task.ReadAll(t => t.schedualedDate == null);
            if (taskList.Count()!=0)
                MessageBox.Show(" Before open the gantt you have to use the automatic schedule");
            else
            new GanttWindow().Show();
        }

        /// <summary>
        /// A button to return all dates from the estimated start to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSchedualeToNull(object sender, RoutedEventArgs e)
        {
            s_bl.Help.SetNullInScheduale();
            MessageBox.Show("success");
        }

        /// <summary>
        /// button to initialize the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickForIntilization(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to do Initialization?", "Initialization", MessageBoxButton.YesNo /*MessageBoxImage.Question*/);
            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                s_bl.Help.init();

            }
        }

        /// <summary>
        /// A button to set start dates for all tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetStartDates(object sender, RoutedEventArgs e)
        {
            try 
            {
                s_bl.Help.SetStartDates();
                MessageBox.Show("success");
            }
           catch(Exception ex) {  MessageBox.Show(ex.Message); }
        }
    }
}
