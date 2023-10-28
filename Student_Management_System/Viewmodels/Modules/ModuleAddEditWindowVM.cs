using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Student_Management_System.Viewmodels.Modules
{
    public partial class ModuleAddEditWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public string code;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public int credit;
        #endregion

        // validate inputs of adding or editing module
        #region Save Command
        [RelayCommand]
        public void Save()
        {
            if (Name == null || Code == null)
            {
                MessageBox.Show("Feilds cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Code.Length > 6)
            {
                MessageBox.Show("Invalid Module Code", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Credit < 1 || Credit > 5)
            {
                MessageBox.Show("Invalid Credits", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                CloseWindow();
        }
        #endregion

        // close the add or edit module window
        #region Cancel Command
        [RelayCommand]
        public void Cancel()
        {
            CloseWindow();
        }
        #endregion

        //close the current window
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
