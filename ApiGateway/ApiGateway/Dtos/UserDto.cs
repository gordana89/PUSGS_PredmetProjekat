using Microsoft.AspNetCore.Http;

namespace ApiGatewayService.Api.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public IFormFile File { get; set; }
    }
}
