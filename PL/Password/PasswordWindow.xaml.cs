using PL.Engineer;
using PL.Manager;
using PL.TaskInList;
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

namespace PL.Password
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public PasswordWindow()
        {
            InitializeComponent();
            User = new BO.User();
        }



        public BO.User User
        {
            get { return (BO.User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(BO.User), typeof(PasswordWindow), new PropertyMetadata(null));

        private void ClickForEnter(object sender, RoutedEventArgs e)
        {
            if (User.Id == 1234 && User.Password == 1234)
            {
                new ManagerWindow().ShowDialog();
            }
            else
            {
                try
                {
                    BO.TaskInEngineer? et = s_bl.Engineer.ReadForPassword(User.Id, User.Password);
                    if (et == null) { new TaskInListWindow(true).Show(); }
                    else
                        new TaskWindow(et.id, true).ShowDialog();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}