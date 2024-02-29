using BlApi;
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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
            CurrentTime = s_bl.clock;
            s_bl.addClockObserver(ClockObserver);
        }





        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(null));



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
                s_bl.Help.init();
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void AddDayClick(object sender, RoutedEventArgs e)
        {
            s_bl.clockForwardDay();
        }

        private void AddHourClick(object sender, RoutedEventArgs e)
        {
            s_bl.clockForwardHour();
        }

        private void AddYearClick(object sender, RoutedEventArgs e)
        {
            s_bl.clockForwardYear();
        }

        private void ClockObserver()
        {
            CurrentTime = s_bl.clock;
        }

        private void ClickForResetClock(object sender, RoutedEventArgs e)
        {
            s_bl.clockInit();
        }
    }
}