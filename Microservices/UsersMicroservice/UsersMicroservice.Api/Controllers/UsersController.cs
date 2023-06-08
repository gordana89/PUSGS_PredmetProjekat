using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersMicroservice.Domain.Dtos;
using UsersMicroservice.Api.Utils;
using UsersMicroservice.Domain.Abstractions;
using System;

namespace UsersMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IMyLogger _myLogger;
        public UsersController(IUsersService userService, IMyLogger myLogger)
        {
            _userService = userService;
            _myLogger = myLogger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            _myLogger.LogInfo($"Get all users {DateTime.Now}");
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email)
        {
            _myLogger.LogInfo($"Get user {email} {DateTime.Now}");
            var user = await _userService.GetUserByEmail(email);

            return Ok(user);
        }

        [HttpGet("{id}/find")]
        public async Task<ActionResult<UserDto>> FindUser(string id)
        {
            var user = await _userService.GetUserById(id);

            return Ok(user);
        }

        [HttpGet("{email}/download")]
        public async Task<IActionResult> GetImage(string email)
        {
            var image = (await PictureHandler.GetImage(email)).Value;
            return File(image, "image/jpeg",email.Split('@')[0]+".jpg");
        }
        [HttpPost ("login")]
        
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            _myLogger.LogInfo($"Login user {loginDTO.Email} {DateTime.Now}");
            string token = (await _userService.Login(loginDTO));
            return Ok(new { token });
        }

        [HttpPost]        
        public async Task<IActionResult> PostUser([FromForm] UserDto userDTO)
        {
            _myLogger.LogInfo($"Created user {userDTO.Email} {DateTime.Now}");
            bool result = (await _userService.Register(userDTO));
            
            if (!result)
                return BadRequest();
            
            return Ok();
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> PutUser(string id,[FromForm] UserDto userDTO)
        {
            _myLogger.LogInfo($"Edited user {id} {DateTime.Now}");
            bool result = (await _userService.Edit(id,userDTO));
            if (!result)
                return BadRequest();
   
            return Ok();
        }

        [HttpPut ("{email}/approve")]
        public async Task<IActionResult> ApproveUser(string email)
        {
            _myLogger.LogInfo($"Approved user {email} {DateTime.Now}");
            bool result = (await _userService.SetStatus(email, "Approved"));
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpPut("{email}/cancel")]
        public async Task<IActionResult> CancelUser(string email)
        {
            _myLogger.LogInfo($"Canceled user {email} {DateTime.Now}");
            bool result = (await _userService.SetStatus(email, "Canceled"));
            if (!result)
                return BadRequest();

            return Ok();
        }

    }
}
