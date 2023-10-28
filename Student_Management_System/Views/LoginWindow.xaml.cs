using Student_Management_System.Models;
using Student_Management_System.Viewmodels.Dashboards;
using Student_Management_System.Views.Dashboards;
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

namespace Student_Management_System.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //to login
        #region Login Button
        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            var usrname = txt_username.Text;
            var passwd = txt_passwd.Password;

            using DbDataContext context = new();

            bool isAdmin = context.DbUsers.Any(admin => admin.Username == usrname && admin.Password == passwd && admin.IsAdmin == true);

            bool isUsr = context.DbUsers.Any(usr => usr.Username == usrname && usr.Password == passwd && usr.IsAdmin == false);

            if (isAdmin)
            {
                User admin = context.DbUsers.SingleOrDefault(admin => admin.Username == usrname && admin.Password == passwd && admin.IsAdmin == true);
                var viewmodel = new DashboardWindowVM { NameUser = admin.Name };
                var adminDashboardWindow = new AdminDashboardWindow { DataContext = viewmodel };
                adminDashboardWindow.ShowDialog();
                Close();
            }
            else if (isUsr)
            {
                User user = context.DbUsers.SingleOrDefault(usr => usr.Username == usrname && usr.Password == passwd && usr.IsAdmin == false);
                var viewmodel = new DashboardWindowVM { NameUser = user.Name };
                var userDashboardWindow = new UserDashboardWindow { DataContext = viewmodel };
                userDashboardWindow.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password", "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        //to Exiit the program
        #region Cancel Button
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            var x = MessageBox.Show("Dou you want to exit?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (x == MessageBoxResult.Yes)
                Close();
        }
        #endregion
    }
}
