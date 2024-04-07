using System;
using System.Collections.Generic;
using System.Data;
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

namespace PL.Gantt
{
    /// <summary>
    /// Interaction logic for GanttWindow.xaml
    /// </summary>
    public partial class GanttWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public GanttWindow()
        {
            InitializeComponent();
        }


        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(GanttWindow), new PropertyMetadata(null));


        private void DataGrid_Initialized_1(object sender, EventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;

            DataTable dataTable = new DataTable();

            if (dataGrid != null)
            {
                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Task Id", Binding = new Binding("[0]") });
                dataTable.Columns.Add("Task Id", typeof(int));

                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Task Name", Binding = new Binding("[1]") });
                dataTable.Columns.Add("Task Name", typeof(string));

                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Dependencies", Binding = new Binding("[2]") });
                dataTable.Columns.Add("Dependencies", typeof(string));

                int col = 3;
                DateTime startDate = s_bl.Clock.GetStartOfProject() ?? DateTime.MinValue;
                for (DateTime day = startDate; day <= s_bl.Clock.GetEndOfProject(); day = day.AddDays(1))
                {
                    string strDay = $"{day.Day}/{day.Month}/{day.Year}";
                    DataGridTemplateColumn column = new DataGridTemplateColumn() { Header = strDay };

                    FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                    textBlockFactory.SetBinding(TextBlock.BackgroundProperty, new Binding($"[{col}]") { Converter = (IValueConverter)dataGrid.FindResource("StatusToColorConverter") });

                    DataTemplate cellTemplate = new DataTemplate();
                    cellTemplate.VisualTree = textBlockFactory;
                    column.CellTemplate = cellTemplate;

                    dataGrid.Columns.Add(column);

                    dataTable.Columns.Add(strDay, typeof(BO.Status));
                    col++;
                }
            }

            IEnumerable<BO.Task> orderedListTasksSchedule = s_bl.Task.ReadFullTask().OrderBy(t => t.schedualedDate);
            foreach (BO.Task task in orderedListTasksSchedule)
            {
                DataRow row = dataTable.NewRow();
                row[0] = task.id;
                row[1] = task.alias;

                // Get the dependencies of the current task
                IEnumerable<BO.TaskInList> dependencies = s_bl.Task.findDependenciesId(task.id);
                string dependenciesString = string.Join(", ", dependencies.Select(d => d.id));
                row[2] = dependenciesString;
                DateTime startDate = s_bl.Clock.GetStartOfProject() ?? DateTime.MinValue;
                for (DateTime day = startDate; day <= s_bl.Clock.GetEndOfProject(); day = day.AddDays(1))
                {
                    string strDay = $"{day.Day}/{day.Month}/{day.Year}";

                    if (day < task.schedualedDate || day > task.forecastDate)
                    {
                        row[strDay] = BO.Status.None;
                    }
                    else
                    {
                        row[strDay] = task.status;
                    }
                }
                dataTable.Rows.Add(row);
            }

            if (dataGrid != null)
            {
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
    }

}

