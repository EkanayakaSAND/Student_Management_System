using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Student_Management_System.Viewmodels.Students
{
    public partial class StudentAddEditWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public string regNumber;

        [ObservableProperty]
        public string firstName;

        [ObservableProperty]
        public string lastName;

        [ObservableProperty]
        public DateOnly dateOfBirth;

        [ObservableProperty]
        public string gender;

        [ObservableProperty]
        public string email;

        [ObservableProperty]
        public string phoneNumber;

        [ObservableProperty]
        public string address;

        [ObservableProperty]
        public DateOnly registeredDate;

        [ObservableProperty]
        public string gurdianName;

        [ObservableProperty]
        public string gurdianPhoneNumber;

        //This collection is for ComboBox Item Source
        public ObservableCollection<string> GenderItems { get; set; } = new ObservableCollection<string> { "Male", "Female" };
        #endregion

        //To validate input to add or edit student
        #region Save Command
        [RelayCommand]
        public void Save()
        {
            if (FirstName == null || LastName == null || PhoneNumber == null || GurdianPhoneNumber == null || Address == null && !(Gender == "Male" || Gender == "Female"))
            {
                MessageBox.Show("Feilds cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                CloseWindow();
            }

        }
        #endregion

        //To close add or edit window
        #region Cancel Command
        [RelayCommand]
        public void Cancel()
        {
            CloseWindow();
        }
        #endregion

        //To close current window
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
