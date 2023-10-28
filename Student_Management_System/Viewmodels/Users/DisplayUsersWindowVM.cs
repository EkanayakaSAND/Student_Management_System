using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using Student_Management_System.Views.Users;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Student_Management_System.Viewmodels.Users
{
    public partial class DisplayUsersWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public int usr_Id;

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

        [ObservableProperty]
        public User selectedUser;

        [ObservableProperty]
        public List<User> users;
        #endregion

        //Default Constructor
        public DisplayUsersWindowVM()
        {
            users = new List<User>();
            Read();
        }
        
        //Add a user to database
        #region User Add Command
        [RelayCommand]
        public void AddUser()
        {
            var viewmodel = new UserAddEditWindowVM();
            var window = new UserAddEditWindow
            {
                DataContext = viewmodel,
                Title = "ADD USER"
            };

            if (window.ShowDialog() == true)
            {
                var user = new User
                {
                    Name = viewmodel.Name,
                    Password = viewmodel.Password,
                    Email = viewmodel.Email,
                    Username = viewmodel.Username,
                    IsAdmin = viewmodel.IsAdmin,
                    PhoneNumber = viewmodel.PhoneNumber,
                    Address = viewmodel.Address,
                };

                if (user.Username != null && user.Password != null)
                {
                    DbDataContext dataContext = new();
                    dataContext.DbUsers.Add(user);
                    dataContext.SaveChanges();
                    MessageBox.Show("User Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Read();
                }
            }
        }
        #endregion

        //Edit a user in databse
        #region User Edit Command
        [RelayCommand]
        public void EditUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Select the User", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var viewmodel = new UserAddEditWindowVM
                {
                    Name = SelectedUser.Name,
                    Email = SelectedUser.Email,
                    Username = SelectedUser.Username,
                    Password = SelectedUser.Password,
                    IsAdmin = SelectedUser.IsAdmin,
                    Address = SelectedUser.Address,
                    PhoneNumber = SelectedUser.PhoneNumber,
                };

                var window = new UserAddEditWindow
                {
                    DataContext = viewmodel,
                    Title = "EDIT USER"
                };

                if (window.ShowDialog() == true)
                {
                    using DbDataContext dataContext = new();
                    User usr = dataContext.DbUsers.Find(SelectedUser.Usr_Id);

                    if (usr != null)
                    {
                        usr.Name = viewmodel.Name;
                        usr.Email = viewmodel.Email;
                        usr.Username = viewmodel.Username;
                        usr.Password = viewmodel.Password;
                        usr.IsAdmin = viewmodel.IsAdmin;
                        usr.Address = viewmodel.Address;
                        usr.PhoneNumber = viewmodel.PhoneNumber;
                        dataContext.SaveChanges();
                        MessageBox.Show("Usr is edited successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Read();
                    }
                }
            }
        }
        #endregion

        //Delete a user in databse
        #region User Delete Command
        [RelayCommand]
        public void DeleteUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Select the user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var x = MessageBox.Show($"Do you want to delete {SelectedUser.Name} ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (x == MessageBoxResult.Yes)
                {
                    using DbDataContext dataContext = new();
                    User usr = dataContext.DbUsers.SingleOrDefault(usr => usr.Usr_Id == SelectedUser.Usr_Id);
                    dataContext.DbUsers.Remove(usr);
                    dataContext.SaveChanges();
                    Read();
                }
            }
        }
        #endregion

        //to get users to the list from database
        private void Read()
        {
            using DbDataContext dataContext = new();
            Users = dataContext.DbUsers.ToList();
        }
    }
}
