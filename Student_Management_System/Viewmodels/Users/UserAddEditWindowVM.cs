using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Student_Management_System.Viewmodels.Users
{
    public partial class UserAddEditWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;

        [ObservableProperty]
        public bool isAdmin;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string phoneNumber;

        [ObservableProperty]
        public string address;
        #endregion

        //Validate inputs for add or edit user
        #region Save Command
        [RelayCommand]
        public void Save()
        {
            if (Username == null || Password == null || PhoneNumber == null || Name == null || Address == null)
            {
                MessageBox.Show("Feilds cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                CloseWindow();
        }
        #endregion

        //to close the window
        #region Cancel Command
        [RelayCommand]
        public void Cancel()
        {
            CloseWindow();
        }
        #endregion

        //to close current window
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
