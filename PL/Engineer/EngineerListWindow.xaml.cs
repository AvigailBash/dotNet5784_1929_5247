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
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow));

        public BO.Engineerlevel Level { get; set; } = BO.Engineerlevel.None;

        /// <summary>
        /// A method that displays the list of all engineers and updates according to the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectEngineerLevelInCombobox(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Level == BO.Engineerlevel.None) ?
           s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.level == Level)!;
        }

        /// <summary>
        /// Viewing all engineers and double clicking opens the update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickOpenEngineerWindowForCreate(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        /// <summary>
        /// Clicking on the solution to add a new engineer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickOpenEngineerWindowForUptade(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? en = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(en!.id).ShowDialog();
            EngineerList = s_bl?.Engineer.ReadAll()!;

        }

        /// <summary>
        /// Filter by engineer name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = (sender as TextBox).Text.ToLower(); // Get the text from the textbox and convert it to lowercase for case-insensitive filtering

            // Filter the EngineerList based on the textbox content
            EngineerList = s_bl?.Engineer.ReadAll()!.Where(engineer => engineer.name.ToLower().Contains(filterText));
        }
    }
}
