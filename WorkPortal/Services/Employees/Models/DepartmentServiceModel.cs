using System.ComponentModel.DataAnnotations;

namespace WorkPortal.Services.Employees.Models
{
    public class DepartmentServiceModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}