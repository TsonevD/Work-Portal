using System.ComponentModel.DataAnnotations;

namespace WorkPortal.Areas.Admin.Models.Employee
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}