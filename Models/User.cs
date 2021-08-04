using System;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    using static DataConstants;
    public class User : IdentityUser
    {
        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsApproved { get; set; }
    }
}
