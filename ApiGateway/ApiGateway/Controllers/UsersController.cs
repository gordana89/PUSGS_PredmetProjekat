using ApiGatewayService.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGatewayService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly string _url;
        
        public UsersController()
        {
            _client = new HttpClient();
            _url = "https://localhost:6001/api/Users";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync(_url);
            var users = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<UserDto>>(users, options);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync($"{_url}/{email}");

            var user = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserDto>(user, options);
        }

        [HttpGet("{email}/download")]
        public async Task<IActionResult> GetImage(string email)
        {

            var response = await _client.GetAsync($"{_url}/{email}/download");

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return File(stream, "image/jpeg", $"{email.Split('@')[0]}.jpg");
            }
            
            return StatusCode((int)response.StatusCode);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var payload = JsonSerializer.Serialize(loginDTO, options);
            var body = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_url + "/login", body);
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<LoginResponse>(content, options);

            return Ok(new { token.Token });
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromForm]UserDto userDTO)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(userDTO.Password), "Password");
            multipartContent.Add(new StringContent(userDTO.Username), "Username");
            multipartContent.Add(new StringContent(userDTO.Email), "Email");
            multipartContent.Add(new StringContent(userDTO.FirstName), "FirstName");
            multipartContent.Add(new StringContent(userDTO.LastName), "LastName");
            multipartContent.Add(new StringContent(userDTO.DateOfBirth), "DateOfBirth");
            multipartContent.Add(new StringContent(userDTO.Address), "Address");
            multipartContent.Add(new StringContent(userDTO.UserType), "UserType");
            if (userDTO.File != null)
                multipartContent.Add(new StreamContent(userDTO.File.OpenReadStream()), "File", userDTO.File.FileName);
            await _client.PostAsync(_url, multipartContent);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromForm] UserDto userDTO)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(userDTO.Id), "Id");
            multipartContent.Add(new StringContent(userDTO.Username), "Username");
            multipartContent.Add(new StringContent(userDTO.Email), "Email");
            multipartContent.Add(new StringContent(userDTO.FirstName), "FirstName");
            multipartContent.Add(new StringContent(userDTO.LastName), "LastName");
            multipartContent.Add(new StringContent(userDTO.DateOfBirth), "DateOfBirth");
            multipartContent.Add(new StringContent(userDTO.Address), "Address");
            multipartContent.Add(new StringContent(userDTO.UserType), "UserType");
            if(userDTO.File != null)
                multipartContent.Add(new StreamContent(userDTO.File.OpenReadStream()), "File", userDTO.File.FileName);

            var response = await _client.PutAsync($"{_url}/{id}", multipartContent);

            return Ok();

        }

        [HttpPut("{email}/approve")]
        public async Task<IActionResult> ApproveUser(string email)
        { 
            await _client.PutAsync($"{_url}/{email}/approve", null);

            return Ok();
        }

        [HttpPut("{email}/cancel")]
        public async Task<IActionResult> CancelUser(string email)
        {
            await _client.PutAsync($"{_url}/{email}/cancel", null);

            return Ok();
        }
    }
}
