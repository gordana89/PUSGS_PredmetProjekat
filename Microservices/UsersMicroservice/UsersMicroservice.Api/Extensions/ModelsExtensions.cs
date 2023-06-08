
using UsersMicroservice.Domain.Dtos;
using UsersMicroservice.Domain.Entities;
using System.Collections.Generic;

namespace UsersMicroservice.Api.Extensions
{
    public static class ModelsExtensions
    {
        public static UserDto ToDto(this User user)
        {
            if (user == null) return null;
            UserDto userDto = new()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserType = user.UserType,
                Status = user.Status,
                Address = user.Address,
                Image = user.Image
            };
            return userDto;
        }
        public static User ToEntity(this UserDto userDto)
        {
            if (userDto == null) return null;
            var appUser = new User()
            {
                UserName = userDto.Username,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Address = userDto.Address,
                DateOfBirth = userDto.DateOfBirth,
                UserType = userDto.UserType,
                Image = userDto.Image,
                Status = userDto.Status
            };
            return appUser;
        }
        
    }
}
