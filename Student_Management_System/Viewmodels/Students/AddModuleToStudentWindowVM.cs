using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Student_Management_System.Viewmodels.Students
{
    public partial class AddModuleToStudentWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public int stdID;

        [ObservableProperty]
        public string stdName;

        [ObservableProperty]
        public string stdRegNo;

        //Item for grade combo box
        [ObservableProperty]
        public List<string> gradeItems;

        [ObservableProperty]
        public string selectedGrade;

        //items for module combo box
        [ObservableProperty]
        public List<Module> moduleItems;

        [ObservableProperty]
        public Module selectedModule;

        //for Listview selected item
        [ObservableProperty]
        public EnrolledModules enrolledSelectedModule;

        //for Listview
        [ObservableProperty]
        public List<EnrolledModules> enrolledModulesList;
        #endregion

        //Default Constructor
        public AddModuleToStudentWindowVM()
        {
            GradeItems = new List<string> { "A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "E" };
            ModuleItems = new List<Module>();
            EnrolledModulesList = new List<EnrolledModules>();
            GetModules();
        }

        //Overloaded Constructor
        public AddModuleToStudentWindowVM(int std_ID)
        {
            GradeItems = new List<string> { "A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "E" };
            ModuleItems = new List<Module>();
            EnrolledModulesList = new List<EnrolledModules>();
            StdID = std_ID;
            GetModules();
            Refresh();
        }

        //To get module from database
        private void GetModules()
        {
            using DbDataContext dataContext = new();
            ModuleItems = dataContext.DbModules.ToList();
        }

        //To get registered modules to the list from database
        #region Refresh Command
        [RelayCommand]
        public void Refresh()
        {
            using DbDataContext dataContext = new();
            var allModules = dataContext.DbEnrolledModules.ToList();
            EnrolledModulesList = new List<EnrolledModules>();
            EnrolledModulesList.Clear();
            foreach ( var module in allModules )
            {
                if(module.Student_Id == StdID)
                {
                    EnrolledModulesList.Add(module);
                }
            }
        }
        #endregion
        
        //To Add modules to a student
        #region Add Command
        [RelayCommand]
        public void Add()
        {
            if (SelectedModule == null)
            {
                MessageBox.Show("Select the module", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SelectedGrade == null)
            {
                MessageBox.Show("Select the grade", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool isFound = false;
                foreach (var module in EnrolledModulesList) 
                {
                    if(module.Module_Id == SelectedModule.Mod_Id)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                {
                    MessageBox.Show("Module already exits", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var enrollModule = new EnrolledModules
                    {
                        Student_Id = StdID,
                        Module_Id = SelectedModule.Mod_Id,
                        ModuleCode = SelectedModule.Code,
                        ModuleName = SelectedModule.Name,
                        Credits = SelectedModule.Credit,
                        Grade = SelectedGrade
                    };


                    using DbDataContext dataContext = new();
                    dataContext.DbEnrolledModules.Add(enrollModule);
                    dataContext.SaveChanges();
                    Refresh();
                    MessageBox.Show("Module added successfully", "info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        #endregion

        //To delete module from a student
        #region Delete Command
        [RelayCommand]
        public void Delete()
        {
            if(EnrolledSelectedModule == null)
            {
                MessageBox.Show("Select the module","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var x = MessageBox.Show($"Do you want to delete {EnrolledSelectedModule.ModuleName} ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(x == MessageBoxResult.Yes)
                {
                    using DbDataContext dataContext = new();

                    EnrolledModules module = dataContext.DbEnrolledModules.Single(mod => mod.Id == EnrolledSelectedModule.Id);
                    dataContext.Remove(module);
                    dataContext.SaveChanges();
                    Refresh();
                }
            }
        }
        #endregion
    }
}
