using System.ComponentModel.DataAnnotations;

namespace UsersMicroservice.Domain.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
