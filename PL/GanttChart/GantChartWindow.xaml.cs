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
        Dictionary<BO.Status, Brush> colorMapping = new Dictionary<BO.Status, Brush>()
                {
                  { BO.Status.None, Brushes.White }, // צבע מותאם אישית
                  { BO.Status.Scheduled,  new SolidColorBrush(Color.FromRgb(70, 137, 130)) }, // צבע מותאם אישית
                  { BO.Status.Done,Brushes.LightGray },
                };

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public GantChartWindow()
        {
            InitializeComponent();

        }



        //private void DataGrid_Initialized(object sender, EventArgs e)
        //{

        //    DataGrid? dataGrid = sender as DataGrid;
        //    if (dataGrid != null)
        //    {
        //        // ניקוי עמודות קיימות
        //        dataGrid.Columns.Clear();

        //        // הגדרת עמודות עבור מזהה משימה ושם משימה
        //        //dataGrid.Columns.Add(new DataGridTextColumn() { Header = "מזהה משימה", Binding = new Binding("[0]") });
        //        //dataGrid.Columns.Add(new DataGridTextColumn() { Header = "שם משימה", Binding = new Binding("[1]") });

        //        // יצירת מילון לקיבוץ תאריכים לפי שבועות
        //        Dictionary<string, List<DateTime>> groups = new Dictionary<string, List<DateTime>>();
        //        DateTime startDate = s_bl.Clock.GetStartOfProject();
        //        DateTime? endDate = s_bl.Clock.GetEndOfProject();

        //        // יצירת עמודות עבור כל שבוע
        //        while (startDate <= endDate)
        //        {
        //            string weekStart = startDate.ToString("dd/MM/yyyy");
        //            string weekEnd = startDate.AddDays(2).ToString("dd/MM/yyyy");
        //            groups.Add($"{weekStart} - {weekEnd}", new List<DateTime>());

        //            for (int i = 0; i < 7; i++)
        //            {
        //                groups[$"{weekStart} - {weekEnd}"].Add(startDate.AddDays(i));
        //            }

        //            startDate = startDate.AddDays(2);
        //        }

        //        // קבלת רשימת משימות מסודרת לפי מזהה
        //        IEnumerable<BO.Task> orderedListTasksSchedule = s_bl.Task.ReadFullTask().OrderBy(t => t.id);

        //        ////// יצירת DataTable והוספת עמודות
        //        DataTable dataTable = new DataTable();
        //        dataTable.Columns.Add("Task Id", typeof(int));
        //        dataTable.Columns.Add("Task Name", typeof(string));

        //        // הוספת עמודות עבור סטטוס כל שבוע
        //        foreach (var group in groups)
        //        {
        //            dataTable.Columns.Add(group.Key, typeof(BO.Status));


        //        }

        //        //// מילוי שורות נתונים עם מידע על משימות ומצב
        //        foreach (BO.Task task in orderedListTasksSchedule)
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row[0] = task.id;
        //            row[1] = task.alias;

        //            foreach (var group in groups)
        //            {
        //                string strDay = group.Value.First().ToString("dd/MM/yyyy");
        //                BO.Status status = BO.Status.None;

        //                foreach (DateTime day in group.Value)
        //                {
        //                    if (day >= task.createdAtDate/*startDate*/ && day <= task.forecastDate)
        //                    {
        //                        status = task.status;
        //                        break;
        //                    }

        //                }

        //                row[group.Key] = status;
        //            }

        //            dataTable.Rows.Add(row);
        //        }

        //        //הגדרת מקור הנתונים של DataGrid
        //        dataGrid.ItemsSource = dataTable.DefaultView;



        //    }
        //}


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


                //dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Engineer Id", Binding = new Binding("[2]") });
                //dataTable.Columns.Add("Engineer Id", typeof(int));

                //dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Engineer Name", Binding = new Binding("[3]") });
                //dataTable.Columns.Add("Engineer Name", typeof(string));

                int col = 2;
                for (DateTime day = s_bl.Clock.GetStartOfProject(); day <= s_bl.Clock.GetEndOfProject(); day = day.AddDays(1))
                {
                    string strDay = $"{day.Day}/{day.Month}/{day.Year}"; //"21/2/2024"
                    dataGrid.Columns.Add(new DataGridTextColumn() { Header = strDay, Binding = new Binding($"[{col}]") });
                    dataTable.Columns.Add(strDay, typeof(BO.Status));// typeof(System.Windows.Media.Color));
                    col++;

                }
            }

            //add ROWS to logic container (data table)
            IEnumerable<BO.Task> orderedListTasksSchedule = s_bl.Task.ReadFullTask().OrderBy(t => t.id);
            foreach (BO.Task task in orderedListTasksSchedule)
            {
                //dataGrid.CellStyle

                DataRow row = dataTable.NewRow();
                row[0] = task.id;
                row[1] = task.alias;
                //row[2] = task.EngineerId;
                //row[3] = task.EngineerName;

                for (DateTime day = s_bl.Clock.GetStartOfProject(); day <= s_bl.Clock.GetEndOfProject(); day = day.AddDays(1))
                {
                    string strDay = $"{day.Day}/{day.Month}/{day.Year}"; //"21/2/2024"

                    if (day < task.schedualedDate || day > task.forecastDate)
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










