using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class Student
    {
        [Key] public int Std_Id { get; set; }
        public string RegNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly RegisteredDate { get; set; }
        public string GurdianName { get; set; }
        public string GurdianPhoneNumber { get; set; }
        public double GPA { get; set; }

    }
}
