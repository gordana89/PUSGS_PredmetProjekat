using UsersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsersMicroservice.Domain.Dtos;
using UsersMicroservice.Api.Enums;
using UsersMicroservice.Domain.Abstractions;

namespace UsersMicroservice.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<User> _userManager;

        public UsersRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> SetStatus(string email, string value)
        {
            User appUser = await _userManager.FindByEmailAsync(email);
            if(appUser != null)
            {
                appUser.Status = value;
                var result = await _userManager.UpdateAsync(appUser);
                if(result.Succeeded) return true;
            }
            return false;
        }

        public async Task<bool> Edit(User appUser)
        {
            User exist = await _userManager.FindByEmailAsync(appUser.Email);
            if(exist != null && appUser != null)
            {
                exist.DateOfBirth = appUser.DateOfBirth;
                exist.FirstName = appUser.FirstName;
                exist.LastName = appUser.LastName;
                exist.Image = appUser.Image;
                exist.UserName = appUser.UserName;
                exist.Address = appUser.Address;
                var result =  await _userManager.UpdateAsync(exist);
                if(result.Succeeded)
                    return true;
            }
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> Login(LoginDto loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("userID", user.Id.ToString()),
                        new Claim("email",user.Email),
                        new Claim("userName",user.UserName),
                        new Claim("firstName",user.FirstName),
                        new Claim("lastName",user.LastName),
                        new Claim("dateOfBirth",user.DateOfBirth),
                        new Claim("role",user.UserType),
                       }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789012345678")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return token;
            }

            return "Incorrect email or password"; 
        }

        public async Task<bool> Register(User appUser, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(appUser, password);
                if (result.Succeeded)
                {
                    string role = UserType.Customer.ToString();
                    if(appUser.UserType == UserType.Administrator.ToString())
                    {
                        role = UserType.Administrator.ToString();
                    }
                    else if(appUser.UserType == UserType.Customer.ToString())
                    {
                        role = UserType.Customer.ToString();
                    }
                    else if(appUser.UserType == UserType.Deliverer.ToString())
                    {
                        role = UserType.Deliverer.ToString();
                    }
                    _userManager.AddToRoleAsync(appUser, role).Wait();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
