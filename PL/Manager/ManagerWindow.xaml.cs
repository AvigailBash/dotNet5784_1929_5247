using PL.Engineer;
using PL.Gantt_chart;
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

        private void ClickForSetStartAndEndDates(object sender, RoutedEventArgs e)
        {
            new SetStartAndEndProject().ShowDialog();
        }

        private void ClickForAutomaticScheduale(object sender, RoutedEventArgs e)
        {
            s_bl.Help.AutomaticScheduale();
            MessageBox.Show("success");
        }

        private void ClickForOpenGant(object sender, RoutedEventArgs e)
        {
            new GantChartWindow().Show();
        }

        private void SetSchedualeToNull(object sender, RoutedEventArgs e)
        {
            s_bl.Help.SetNullInScheduale();
            MessageBox.Show("success");
        }

        private void ClickForIntilization(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to do Initialization?", "Initialization", MessageBoxButton.YesNo /*MessageBoxImage.Question*/);
            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                s_bl.Help.init();

            }
        }
    }
}
