using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    public class DbDataContext : DbContext
    {
        //Change this path to your database full path
        private readonly string _path = @"F:\UOR\FacultyOfEngineering\Semester_03\Projects\Student_Management_System\Student_Management_System\Database.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {_path}");
        }

        public DbSet<User> DbUsers { get; set; }
        public DbSet<Student> DbStudents { get; set; }
        public DbSet<EnrolledModules> DbEnrolledModules { get; set; }
        public DbSet<Models.Module> DbModules { get; set; }
    }
}
