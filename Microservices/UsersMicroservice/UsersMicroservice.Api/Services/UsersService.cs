using UsersMicroservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersMicroservice.Domain.Dtos;
using UsersMicroservice.Api.Enums;
using UsersMicroservice.Api.Exceptions;
using UsersMicroservice.Api.Utils;
using UsersMicroservice.Api.Extensions;
using UsersMicroservice.Domain.Abstractions;

namespace UsersMicroservice.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMailService _mailService;

        public UsersService(IUsersRepository usersRepository,
            IMailService mailService)
        {
            _usersRepository = usersRepository;
            _mailService = mailService;
        }

        private void CheckImage(UserDto userDto, string oldImage)
        {
            if (userDto.File != null)
            {
                bool ret = PictureHandler.SaveImage(userDto.File, userDto.Email);
                if (ret)
                {
                    userDto.Image = userDto.Email.Split('@')[0] + ".jpg";
                }
                return;
            }
            userDto.Image = oldImage;
            
        }

        private void CheckRole(string role)
        {
            try
            {
                Enum.Parse(typeof(UserType), role);
            }
            catch
            {
                throw new BadRequestException($"Role: {role} is incorrect");
            }
        }
        public async Task<bool> SetStatus(string email, string value)
        {
            var user = (await _usersRepository.GetUserByEmail(email));
            if (user == null)
                throw new NotFoundException($"User with email: {email} doesn't exist");

            if (user.Status == value)
                throw new AlreadyExistsException($"User with email: {email} is already {value}");

            _mailService.SendMail(email, user.FirstName, user.LastName);

            var success = await _usersRepository.SetStatus(email, value);
            if (success)
            {
                var user1 = await _usersRepository.GetUserByEmail(email);
                _mailService.SendMail(email, user1.FirstName, user1.LastName);
            }
            return success;
        }

        public async Task<bool> Edit(string id,UserDto userDTO)
        {
            if (id != userDTO.Id)
            {
                throw new BadRequestException("Id in url and request body must be the same");
            }
            User user = (await _usersRepository.GetUserById(id));
            if(user == null)
            {
                throw new NotFoundException($"User with id: {id} doesn't not exist");
            }
            CheckImage(userDTO, user.Image);
            User appUser = userDTO.ToEntity();

            return await _usersRepository.Edit(appUser);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            User appUser = (await _usersRepository.GetUserByEmail(email));
            if (appUser == null)
                throw new NotFoundException($"User with mail: {email} doesn't exist");

            return appUser.ToDto();
        }

        public async Task<string> Login(LoginDto loginDTO)
        {
            return await _usersRepository.Login(loginDTO);
        }

        public async Task<bool> Register(UserDto userDTO)
        {
            User userExist = (await _usersRepository.GetUserByEmail(userDTO.Email));
            if(userExist != null)
            {
                throw new AlreadyExistsException($"User with email: {userDTO.Email} already exist");
            }

            CheckRole(userDTO.UserType);
            CheckImage(userDTO, null);
            
            User appUser = userDTO.ToEntity();
            appUser.Status = appUser.UserType == "Deliverer" ? "Waiting" : "Approved";
            
            return await _usersRepository.Register(appUser, userDTO.Password);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var appUsers = (await _usersRepository.GetAllUsers());
            
            var usersDTO = new List<UserDto>();
            
            foreach (var item in appUsers)
            {
                usersDTO.Add(item.ToDto());
            }

            return usersDTO;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var appUser = await _usersRepository.GetUserById(id);

            if (appUser == null) return null;
            
            return appUser.ToDto();
        }
    }
}
