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

namespace PL.Gantt_chart
{
    /// <summary>
    /// Interaction logic for GantChartWindow.xaml
    /// </summary>
    public partial class GantChartWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public GantChartWindow()
        {
            InitializeComponent();
        }

       

        private void DataGrid_Initialized(object sender, EventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid; //the graphic container

            DataTable dataTable = new DataTable(); //the logic container

            //add COLUMNS to datagrid and datatable
            if (dataGrid != null)
            {
                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Task Id", Binding = new Binding("[0]") });
                dataTable.Columns.Add("Task Id", typeof(int));

                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Task Name", Binding = new Binding("[1]") });
                dataTable.Columns.Add("Task Name", typeof(string));

                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Engineer Id", Binding = new Binding("[2]") });
                dataTable.Columns.Add("Engineer Id", typeof(int));

                dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Engineer Name", Binding = new Binding("[3]") });
                dataTable.Columns.Add("Engineer Name", typeof(string));

                int col = 4;
                for (DateTime day = s_bl.Clock.GetStartOfProject(); day <= s_bl.Clock.GetEndOfProject(); day = day.AddDays(1))
                {
                    string strDay = $"{day.Day}/{day.Month}/{day.Year}"; //"21/2/2024"
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = strDay, Binding = new Binding($"[{col}]") });
                    dataTable.Columns.Add(strDay, typeof(BO.Status));// typeof(System.Windows.Media.Color));
                    col++;
                }
            }

            //add ROWS to logic container (data table)
            IEnumerable<BO.Task> orderedlistTasksScheduale = s_bl.Task.ReadFullTask().OrderBy(t => t.startDate);
            foreach (BO.Task task in orderedlistTasksScheduale)
            {
                //dataGrid.CellStyle

                DataRow row = dataTable.NewRow();
                row[0] = task.id;
                row[1] = task.alias;
                //row[2] = task.engineer.id;
                //row[3] = task.EngineerName;

                for (DateTime day = s_bl.Clock.GetStartOfProject(); day <= s_bl.Clock.GetEndOfProject(); day = day.AddDays(1))
                {
                    string strDay = $"{day.Day}/{day.Month}/{day.Year}"; //"21/2/2024"

                    if (day < task.startDate || day > task.forecastDate)
                        row[strDay] = BO.Status.None; //"EMPTY";
                    else
                    {

                        row[strDay] = task.status; //BO.TaskStatus.TaskIsSchedualed; //"FULL";
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
