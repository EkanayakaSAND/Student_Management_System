using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using Student_Management_System.Views.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Student_Management_System.Viewmodels.Modules
{
    public partial class DisplayModulesWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public int mod_Id;

        [ObservableProperty]
        public string code;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public int credit;

        [ObservableProperty]
        public List<Module> modules;

        [ObservableProperty]
        public Module selectedModule;
        #endregion

        //Default Constructor
        public DisplayModulesWindowVM()
        {
            Modules = new List<Module>();
            Read();
        }

        // add modules to the database
        #region Module Add Command
        [RelayCommand]
        public void AddModule()
        {
            var viewmodel = new ModuleAddEditWindowVM();
            var window = new ModuleAddEditWindow
            {
                DataContext = viewmodel,
                Title = "ADD MODULE"
            };

            if (window.ShowDialog() == true)
            {
                var module = new Module()
                {
                    Name = viewmodel.Name,
                    Code = viewmodel.Code,
                    Credit = viewmodel.Credit,
                };

                if (module.Code != null && module.Name != null)
                {
                    using DbDataContext dataContext = new();
                    dataContext.DbModules.Add(module);
                    dataContext.SaveChanges();
                    MessageBox.Show("Module Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Read();
                }
            }
        }
        #endregion

        // edit modules in the database
        #region Module Edit Command
        [RelayCommand]
        public void EditModule()
        {
            if (SelectedModule == null)
            {
                MessageBox.Show("Select the Module", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var viewmodel = new ModuleAddEditWindowVM
                {
                    Code = SelectedModule.Code,
                    Name = SelectedModule.Name,
                    Credit = SelectedModule.Credit,
                };

                var window = new ModuleAddEditWindow { DataContext = viewmodel, Title = "EDIT MODULE" };

                if (window.ShowDialog() == true)
                {
                    using DbDataContext dataContext1 = new();
                    Module module = dataContext1.DbModules.Find(SelectedModule.Mod_Id);
                    if (module.Code != null || module.Name != null)
                    {
                        module.Code = viewmodel.Code;
                        module.Name = viewmodel.Name;
                        module.Credit = viewmodel.Credit;
                        dataContext1.SaveChanges();
                        MessageBox.Show("Module is edited successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Read();
                    }
                }
            }
        }
        #endregion

        // delete modules in the database
        #region Module Delete Command
        [RelayCommand]
        public void DeleteModule()
        {
            if (SelectedModule == null)
            {
                MessageBox.Show("Select the module", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var x = MessageBox.Show($"Do you want to delete {SelectedModule.Name} ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (x == MessageBoxResult.Yes)
                {
                    using DbDataContext dataContext = new();
                    Module module = dataContext.DbModules.SingleOrDefault(mod => mod.Mod_Id == SelectedModule.Mod_Id);
                    dataContext.DbModules.Remove(module);
                    dataContext.SaveChanges();
                    Read();
                }
            }
        }
        #endregion

        // get modules to the list from database
        private void Read()
        {
            using DbDataContext dataContext = new();
            Modules = dataContext.DbModules.ToList();
        }
    }
}
