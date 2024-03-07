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



        public DateTime start { get; set; } = s_bl.clock;
        public DateTime end { get; set; } = s_bl.clock;

        private void SetDates(object sender, RoutedEventArgs e)
        {
            s_bl.Clock.SetStartOfProject(start);
            s_bl.Clock.SetEndOfProject(end);
            MessageBox.Show("Success");
            this.Close();   
        }
    }
}
