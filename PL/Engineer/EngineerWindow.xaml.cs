using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
            {
                Engineer = new BO.Engineer();
                Task = new BO.Task();
            }
            else
            {
                try
                {
                    Engineer = s_bl.Engineer.Read(Id)!;
                    if(Engineer.task==null)
                    {
                        Engineer.task = new BO.TaskInEngineer();
                    }
                    else
                    {
                        Engineer.task = s_bl.Task.ReadInTaskInEngineerFormat(Engineer.task.id);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }

            }

        }
       

        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }
        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register(nameof(Engineer), typeof(BO.Engineer), typeof(EngineerWindow));

        //public IEnumerable<BO.TaskInList> TaskInLists
        //{
        //    get { return (IEnumerable<BO.TaskInList>)GetValue(taskInListsProperty); }
        //    set { SetValue(taskInListsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TaskInLists.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty taskInListsProperty =
        //    DependencyProperty.Register(nameof(TaskInLists), typeof(IEnumerable<BO.TaskInList>), typeof(EngineerWindow), new PropertyMetadata(null));


        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(EngineerWindow), new PropertyMetadata(null));



        private void clickForUpdateOrAdd(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            try
            {
               
                if (button is { Content: "Add"})
                {
                    if(!(Engineer.id is  DigitShapes) || Engineer.password==null)
                    {
                        throw new Exception("One of the details is incorrect");
                    }
                    s_bl.Engineer.Create(Engineer);
                }
                else
                {
                    if (Engineer.id is DigitShapes || Engineer.password == null)
                    {
                        throw new Exception("One of the details is incorrect");
                    }
                    s_bl.Engineer.Update(Engineer);
                }
                MessageBox.Show("success");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); this.Close(); };
        }

       
    }
}
