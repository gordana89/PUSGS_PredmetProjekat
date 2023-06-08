using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrdersMicroservice.Domain.Dtos;
using OrdersMicroservice.Domain.Abstractions;
using System;

namespace OrdersMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IMyLogger _myLogger;
        public OrdersController(IOrdersService ordersService, IMyLogger myLogger)
        {
            _ordersService = ordersService;
            _myLogger = myLogger;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            _myLogger.LogInfo($"Get all Orders {DateTime.Now}");
            var orders = await _ordersService.GetAll();
            
            return Ok(orders);
        }

        [HttpGet ("{customerId}/customer")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerId(string customerId)
        {
            _myLogger.LogInfo($"Get orders for customer {customerId} {DateTime.Now}");
            var orders = await _ordersService.GetOrderByCustomerId(customerId);
            
            return Ok(orders);
        }

        [HttpGet ("{delivererId}/deliverer")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByDelivererId(string delivererId)
        {
            _myLogger.LogInfo($"Get orders for deliverer {DateTime.Now}");
            var orders = await _ordersService.GetOrderByDelivererId(delivererId);
            
            return Ok(orders);
        }


        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrders(OrderDto orderDTO)
        {
            _myLogger.LogInfo($"Create new order {DateTime.Now}");
            var created = await _ordersService.Add(orderDTO);
            
            return Ok(created);
        }

        [HttpPut ("{id}/take")]
        public async Task<ActionResult<OrderDto>> TakeOrder(int id, [FromBody]TakeOrderDto order)
        {
            _myLogger.LogInfo($"Take order with Id:{id} {DateTime.Now}");
            return await _ordersService.Take(id, order);
        }

        [HttpPut("{id}/archive")]
        public async Task<ActionResult<OrderDto>> ArchiveOrder(int id)
        {
            _myLogger.LogInfo($"Archive order with Id:{id} {DateTime.Now}");
            var archived = await _ordersService.Archive(id);

            return Ok(archived);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDto>> DeleteOrder(int id)
        {
            _myLogger.LogInfo($"Delete order with Id:{id} {DateTime.Now}");
            var deleted = await _ordersService.Delete(id);

            return Ok(deleted);
        }

    }
}
