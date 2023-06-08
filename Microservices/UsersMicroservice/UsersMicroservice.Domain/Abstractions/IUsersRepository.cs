using UsersMicroservice.Domain.Dtos;
using UsersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersMicroservice.Domain.Abstractions
{
    public interface IUsersRepository
    {
        Task<string> Login(LoginDto loginDTO);
        Task<bool> Register(User appUser, string password);
        Task<bool> Edit(User appUser);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(string id);
        Task<bool> SetStatus(string email, string value);
    }
}
