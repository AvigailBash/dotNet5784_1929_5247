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
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerListWindow()
        {
            InitializeComponent();
            engineer = s_bl?.Engineer.ReadAll()!;
        }

        public IEnumerable<BO.Engineer> engineer
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(engineerProperty); }
            set { SetValue(engineerProperty, value); }
        }

        public static readonly DependencyProperty engineerProperty =
            DependencyProperty.Register("engineer", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.Engineerlevel Level { get; set; } = BO.Engineerlevel.None;

        private void SelectEngineerLevelInCombobox(object sender, SelectionChangedEventArgs e)
        {
            engineer = (Level == BO.Engineerlevel.None) ?
           s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.level == Level)!;

        }
    }



}
