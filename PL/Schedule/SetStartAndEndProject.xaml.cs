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

namespace PL.Schedule
{
    /// <summary>
    /// Interaction logic for  SetStartAndEndProject.xaml
    /// </summary>
    public partial class SetStartAndEndProject : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public SetStartAndEndProject()
        {
            InitializeComponent();
            
        }



        public DateTime? start { get; set; } =  s_bl.Clock.GetStartOfProject();
        public DateTime? end { get; set; } = s_bl.Clock.GetEndOfProject();

        private void SetDates(object sender, RoutedEventArgs e)
        {
            try
            {
                if (end <= start) { throw new Exception("The dates is incorrect"); }
                s_bl.Clock.SetStartOfProject(start);
                s_bl.Clock.SetEndOfProject(end);
                MessageBox.Show("Success");
                this.Close();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SetDetsToBeNullClick(object sender, RoutedEventArgs e)
        {
            DateTime? d;
           d= s_bl.Clock.SetStartOfProject(null);
           d=s_bl.Clock.SetEndOfProject(null);
            start = s_bl.Clock.GetStartOfProject(); 
            end = s_bl.Clock.GetEndOfProject();
        }
    }
}
