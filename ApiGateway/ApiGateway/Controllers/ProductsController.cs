using ApiGatewayService.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGatewayService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public ProductsController()
        {
            _client = new HttpClient();
            _url = "https://localhost:7001/api/Products";
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync(_url);
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<ProductDto>>(content, serializeOptions);

            return products;
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync(_url + $"/{id}");

            if (response.StatusCode != HttpStatusCode.OK)
                return NotFound("Product doesn't exist");

            var content = await response.Content.ReadAsStringAsync();

            var product = JsonSerializer.Deserialize<ProductDto>(content, options);

            return product;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var payload = JsonSerializer.Serialize(productDto, options);
            var body = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_url, body);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(JsonSerializer.Deserialize<ProductDto>(content, options));
        }

        [HttpPut ("{id}")]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<ProductDto>> EditProduct(int id, ProductDto productDto)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var payload = JsonSerializer.Serialize(productDto, options);
            var body = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_url + $"/{id}", body);

            if (response.StatusCode != HttpStatusCode.OK)
                return NotFound("Product doesn't exist");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(JsonSerializer.Deserialize<ProductDto>(content, options));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.DeleteAsync(_url + $"/{id}");

            if (response.StatusCode != HttpStatusCode.OK)
                return NotFound("Product doesn't exist");

            var content = await response.Content.ReadAsStringAsync();

            var product = JsonSerializer.Deserialize<ProductDto>(content, options);

            return product;
        }
    }
}
