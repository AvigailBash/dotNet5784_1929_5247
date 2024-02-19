using PL.Engineer;
using PL.Manager;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToEngineerWiew(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();

        }

        private void DoInitialization(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to do Initialization?", "Initialization", MessageBoxButton.YesNo /*MessageBoxImage.Question*/);
            // Check the user's response
            if (result == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }
    }
}