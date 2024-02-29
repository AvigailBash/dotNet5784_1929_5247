using BO;
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

namespace PL.Dependencies
{
    /// <summary>
    /// Interaction logic for DependenciesListWindow.xaml
    /// </summary>
    public partial class DependenciesListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public DependenciesListWindow(int id=0 )
        {
            InitializeComponent();
            if (id == 0)
                Dependencies = null;
            else
            Dependencies = s_bl?.Task.Read(id)!.dependencies!;

        }







        public IEnumerable<BO.TaskInList> Dependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(DependenciesProperty); }
            set { SetValue(DependenciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dependencies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DependenciesProperty =
            DependencyProperty.Register("Dependencies", typeof(IEnumerable<BO.TaskInList>), typeof(DependenciesListWindow), new PropertyMetadata(null));

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
