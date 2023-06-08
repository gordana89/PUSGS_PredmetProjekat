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
    public class OrdersController : ControllerBase
    {
        private readonly string _url;
        private readonly HttpClient _client;
        
        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _client = new HttpClient();
            _url = "https://localhost:7001/api/Orders";
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync(_url);
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<OrderDto>>(content, serializeOptions);

            return orders;
        }

        [HttpGet ("{customerId}/customer")]
        [Authorize]

        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomer(string customerId)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync(_url + $"/{customerId}/customer");
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<OrderDto>>(content, serializeOptions);

            return orders;
        }

        [HttpGet("{delivererId}/deliverer")]
        [Authorize]

        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByDeliverer(string delivererId)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.GetAsync(_url + $"/{delivererId}/deliverer");
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<OrderDto>>(content, serializeOptions);

            return orders;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<OrderDto>> PostOrders(OrderDto orderDTO)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var payload = JsonSerializer.Serialize(orderDTO, options);
            var body = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_url, body);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(JsonSerializer.Deserialize<OrderDto>(content, options));
        }

        [HttpPut("{id}/take")]
        [Authorize(Roles = "Deliverer")]

        public async Task<ActionResult<OrderDto>> TakeOrder(int id, [FromBody] TakeOrderDto order)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var payload = JsonSerializer.Serialize(order, options);
            var body = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_url + $"/{id}/take", body);

            if (response.StatusCode != HttpStatusCode.OK)
                return NotFound("Order doesn't exist");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(JsonSerializer.Deserialize<OrderDto>(content, options));
        }

        [HttpPut("{id}/archive")]
        [Authorize(Roles = "Deliverer")]

        public async Task<ActionResult<OrderDto>> ArchiveOrder(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.PutAsync(_url + $"/{id}/archive", null);
            if (response.StatusCode != HttpStatusCode.OK)
                return NotFound("Product doesn't exist");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(JsonSerializer.Deserialize<OrderDto>(content, options));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]

        public async Task<ActionResult<OrderDto>> DeleteOrder(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var response = await _client.DeleteAsync(_url + $"/{id}");
            if (response.StatusCode != HttpStatusCode.OK)
                return NotFound("Order doesn't exist");
            var content = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<OrderDto>(content, options);

            return product;
        }
    }
}
