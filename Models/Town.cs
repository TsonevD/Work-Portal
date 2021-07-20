using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Models
{
    using static DataConstants;
    public class Town
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(TownNameMaxLength)]
        public string Name { get; set; }
    }
}