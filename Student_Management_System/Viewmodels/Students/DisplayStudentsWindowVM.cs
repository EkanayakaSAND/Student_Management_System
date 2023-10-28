using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Student_Management_System.Models;
using Student_Management_System.Views.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Student_Management_System.Viewmodels.Students
{
    public partial class DisplayStudentsWindowVM : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        public int std_Id;

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

        [ObservableProperty]
        public double gPA;

        //students for listview
        [ObservableProperty]
        public List<Student> students;

        [ObservableProperty]
        public Student selectedStudent;
        #endregion

        //Default Constructor
        public DisplayStudentsWindowVM()
        {
            students = new List<Student>();
            Read();
        }

        //To Add students to the database
        #region Student Add Command
        [RelayCommand]
        public void AddStudent()
        {
            var viewmodel = new StudentAddEditWindowVM();
            var window = new StudentAddEditWindow
            {
                DataContext = viewmodel,
                Title = "ADD STUDENT"
            };

            if (window.ShowDialog() == true)
            {
                var student = new Student
                {
                    RegNumber = viewmodel.RegNumber,
                    FirstName = viewmodel.FirstName,
                    LastName = viewmodel.LastName,
                    DateOfBirth = viewmodel.DateOfBirth,
                    Gender = viewmodel.Gender,
                    Email = viewmodel.Email,
                    PhoneNumber = viewmodel.PhoneNumber,
                    Address = viewmodel.Address,
                    RegisteredDate = viewmodel.RegisteredDate,
                    GurdianName = viewmodel.GurdianName,
                    GurdianPhoneNumber = viewmodel.GurdianPhoneNumber,
                };

                if (student.FirstName != null && student.RegNumber != null && student.LastName != null && student.Email != null)
                {
                    using DbDataContext dataContext = new();
                    dataContext.DbStudents.Add(student);
                    dataContext.SaveChanges();
                    MessageBox.Show("Student Added Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    Read();
                }
            }
        }
        #endregion

        //To Edit a student in databse
        #region Student Edit Command
        [RelayCommand]
        public void EditStudent()
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Select the Student", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var viewmodel = new StudentAddEditWindowVM
                {
                    RegNumber = SelectedStudent.RegNumber,
                    FirstName = SelectedStudent.FirstName,
                    LastName = SelectedStudent.LastName,
                    DateOfBirth = SelectedStudent.DateOfBirth,
                    Gender = SelectedStudent.Gender,
                    Email = SelectedStudent.Email,
                    PhoneNumber = SelectedStudent.PhoneNumber,
                    Address = SelectedStudent.Address,
                    RegisteredDate = SelectedStudent.RegisteredDate,
                    GurdianName = SelectedStudent.GurdianName,
                    GurdianPhoneNumber = SelectedStudent.GurdianPhoneNumber,
                };

                var window = new StudentAddEditWindow
                {
                    DataContext = viewmodel,
                    Title = "EDIT STUDENT"
                };

                if (window.ShowDialog() == true)
                {
                    using DbDataContext dataContext = new();
                    Student student = dataContext.DbStudents.Find(SelectedStudent.Std_Id);

                    if (student != null)
                    {
                        student.RegNumber = viewmodel.RegNumber;
                        student.FirstName = viewmodel.FirstName;
                        student.LastName = viewmodel.LastName;
                        student.DateOfBirth = viewmodel.DateOfBirth;
                        student.Gender = viewmodel.Gender;
                        student.Email = viewmodel.Email;
                        student.PhoneNumber = viewmodel.PhoneNumber;
                        student.Address = viewmodel.Address;
                        student.RegisteredDate = viewmodel.RegisteredDate;
                        student.GurdianName = viewmodel.GurdianName;
                        student.GurdianPhoneNumber = viewmodel.GurdianPhoneNumber;

                        dataContext.SaveChanges();
                        MessageBox.Show("Student Edited Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        Read();
                    }
                }
            }
        }
        #endregion

        //To delete a student in databse
        #region Student Delete Command
        [RelayCommand]
        public void DeleteStudent()
        {
            using DbDataContext dataContext = new();

            if (SelectedStudent != null)
            {
                var x = MessageBox.Show($"Do you want to delete {SelectedStudent.FirstName} ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (x == MessageBoxResult.Yes)
                {
                    Student student = dataContext.DbStudents.Single(std => std.Std_Id == SelectedStudent.Std_Id);
                    dataContext.Remove(student);
                    dataContext.SaveChanges();
                    Read();
                }
            }
        }
        #endregion

        //To add modules to a student
        #region Add Module Command
        [RelayCommand]
        public void AddModules()
        {

            if (SelectedStudent == null)
            {
                MessageBox.Show("Select the student", "Eorror", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var viewmodel = new AddModuleToStudentWindowVM(SelectedStudent.Std_Id)
                { 
                    StdName = SelectedStudent.FirstName,
                    StdRegNo = SelectedStudent.RegNumber
                };

                var window = new AddModuleToStudentWindow { DataContext = viewmodel };

                window.ShowDialog();
                GPA = CalcGPA();
                using DbDataContext dataContext = new();
                var student = dataContext.DbStudents.SingleOrDefault(std => std.Std_Id == SelectedStudent.Std_Id);

                if(student != null)
                    student.GPA = GPA;

                dataContext.SaveChanges();
                Read();
            }
        }
        #endregion

        //To calculate GPA
        public double CalcGPA()
        {
            using DbDataContext dataContext = new();
            var allModules = dataContext.DbEnrolledModules.ToList();

            List<EnrolledModules> registeredModules = new();

            foreach (var mod in allModules)
            {
                if (SelectedStudent.Std_Id == mod.Student_Id)
                {
                    registeredModules.Add(mod);
                }
            }

            double totalGradePoints = 0;
            double totalCredits = 0;

            if (registeredModules != null)
            {
                foreach (var module in registeredModules)
                {
                    int credit = module.Credits;
                    string grade = module.Grade;
                    double gradePoint = 0;

                    switch (grade)
                    {
                        case "A+":
                            gradePoint = 4.0;
                            break;
                        case "A":
                            gradePoint = 4.0;
                            break;
                        case "A-":
                            gradePoint = 3.7;
                            break;
                        case "B+":
                            gradePoint = 3.3;
                            break;
                        case "B":
                            gradePoint = 3.0;
                            break;
                        case "B-":
                            gradePoint = 2.7;
                            break;
                        case "C+":
                            gradePoint = 2.3;
                            break;
                        case "C":
                            gradePoint = 2.0;
                            break;
                        case "C-":
                            gradePoint = 1.7;
                            break;
                        case "E":
                            gradePoint = 0.0;
                            break;
                    }

                    totalGradePoints += credit * gradePoint;
                    totalCredits += credit;
                }
                return Math.Round(totalGradePoints / totalCredits, 2);
            }
            return 0;
        }

        //To get students to the list from database
        private void Read()
        {
            using DbDataContext dataContext = new();
            Students = dataContext.DbStudents.ToList();
        }
    }
}
