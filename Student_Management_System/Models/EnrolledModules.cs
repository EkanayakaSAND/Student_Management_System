using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class EnrolledModules
    {
        [Key] public int Id { get; set; }
        public int Student_Id { get; set; }
        public int Module_Id { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int Credits { get; set; }
        public string Grade { get; set; }
    }
}
