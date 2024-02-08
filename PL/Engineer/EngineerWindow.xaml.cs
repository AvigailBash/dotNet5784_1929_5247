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

     
        public BO.Engineer engineer
        {
            get { return (BO.Engineer)GetValue(engineerProperty); }
            set { SetValue(engineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for engineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty engineerProperty =
            DependencyProperty.Register("engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(0));


        public EngineerWindow(int Id = 0)
        {  
           InitializeComponent();
            if(Id == 0)
            {
                engineer=new BO.Engineer();
            }
            else
            {
                try
                {
                    engineer = s_bl.Engineer.Read(Id);
                }
                catch(Exception ex) { Console.WriteLine(ex); }
                    
            }

        }

       
    }
}
