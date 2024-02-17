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
            }
            else
            {
                try
                {
                    Engineer = s_bl.Engineer.Read(Id)!;
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

        public IEnumerable<BO.TaskInList> TaskInLists
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(taskInListsProperty); }
            set { SetValue(taskInListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskInLists.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty taskInListsProperty =
            DependencyProperty.Register(nameof(TaskInLists), typeof(IEnumerable<BO.TaskInList>), typeof(EngineerWindow), new PropertyMetadata(null));
       

        private void clickForUpdateOrAdd(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            try
            {
                if (button is { Content: "Add"})
                {
                    s_bl.Engineer.Create(Engineer);
                }
                else
                {
                    s_bl.Engineer.Update(Engineer);
                }
                MessageBox.Show("success");
                this.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }
    }
}
