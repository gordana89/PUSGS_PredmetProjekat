using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UsersMicroservice.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string UserType { get; set; }

        public string Image { get; set; }

        public string Status { get; set; }
    }
}
