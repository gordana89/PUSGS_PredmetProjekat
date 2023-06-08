using UsersMicroservice.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersMicroservice.Domain.Abstractions
{
    public interface IUsersService
    {
        Task<string> Login(LoginDto loginDto);
        Task<bool> Register(UserDto userDto);
        Task<bool> Edit(string id, UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserById(string id);
        Task<bool> SetStatus(string email, string value);

    }
}
