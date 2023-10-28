using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using Student_Management_System.Viewmodels.Modules;
using Student_Management_System.Viewmodels.Students;
using Student_Management_System.Viewmodels.Users;
using Student_Management_System.Views;
using Student_Management_System.Views.Modules;
using Student_Management_System.Views.Students;
using Student_Management_System.Views.Users;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Student_Management_System.Viewmodels.Dashboards
{
    public partial class DashboardWindowVM : ObservableObject
    {
        //Default Constructor
        public DashboardWindowVM()
        {
            getCounts();
        }

        #region Properties
        List<Student> students;
        List<User> users;
        List<Module> modules;

        [ObservableProperty]
        int studentCount;

        [ObservableProperty]
        int userCount;

        [ObservableProperty]
        int moduleCount;

        [ObservableProperty]
        string nameUser;
        #endregion

        // open window that displays details of all students
        #region Display Student Command
        [RelayCommand]
        public void DisplayStudents()
        {
            var viewmodel = new DisplayStudentsWindowVM();
            var window = new DisplayStudentsWindow { DataContext = viewmodel };

            window.ShowDialog();
            getCounts();        //To Refresh counts after closing window
        }
        #endregion

        // open window that displays details of all users
        #region Display Users Command
        [RelayCommand]
        public void DisplayUsers()
        {
            var viewmodel = new DisplayUsersWindowVM();
            var window = new DisplayUsersWindow { DataContext = viewmodel };

            window.ShowDialog();
            getCounts();        //To Refresh counts after closing window
        }
        #endregion

        // open window that displays details of all modules
        #region Display Modules Command
        [RelayCommand]
        public void DisplayModules()
        {
            var viewmodel = new DisplayModulesWindowVM();
            var window = new DisplayModulesWindow { DataContext = viewmodel };

            window.ShowDialog();
            getCounts();        //To Refresh counts after closing window
        }
        #endregion

        // Logout the current system user
        #region Logout Command
        [RelayCommand]
        public void Logout()
        {
            var x = MessageBox.Show("Do you want Logout?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (x == MessageBoxResult.Yes)
            {
                CloseWindow();
                LoginWindow window = new LoginWindow();
                window.Show();
            }
        }
        #endregion

        // get all counts of users,students,modules from database
        private void getCounts()
        {
            students = new List<Student>();
            users = new List<User>();
            modules = new List<Module>();

            using DbDataContext dbDataContext = new DbDataContext();
            students = dbDataContext.DbStudents.ToList();
            StudentCount = students.Count;

            users = dbDataContext.DbUsers.ToList();
            UserCount = users.Count;

            modules = dbDataContext.DbModules.ToList();
            ModuleCount = modules.Count;

        }

        // close the current window
        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
        }       
    }
   
}
