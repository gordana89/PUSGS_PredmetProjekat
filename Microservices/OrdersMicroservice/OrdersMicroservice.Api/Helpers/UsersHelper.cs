using OrdersMicroservice.Domain.Dtos;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrdersMicroservice.Api.Helpers
{
    public static class UsersHelper
    {
        public static async Task<UserDto> GetUser(string id)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            if (id == null) return null;
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:6001/api/Users"+$"/{id}/find");

            var user = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserDto>(user, serializeOptions);
        }
    }
}
