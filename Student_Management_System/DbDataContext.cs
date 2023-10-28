using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;

namespace Student_Management_System
{
    public class DbDataContext : DbContext
    {
        //Change this path to your database full path
        private readonly string _path = @"<Add your absolute path to database>";

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
