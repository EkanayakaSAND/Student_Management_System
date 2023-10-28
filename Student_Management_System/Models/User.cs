using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class User
    {
        [Key] public int Usr_Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
