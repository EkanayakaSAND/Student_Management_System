using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class Module
    {
        [Key]
        public int Mod_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }
}
