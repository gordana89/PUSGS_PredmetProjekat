using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrdersMicroservice.Domain.Dtos;
using OrdersMicroservice.Api.Exceptions;
using OrdersMicroservice.Api.Extensions;
using OrdersMicroservice.Domain.Abstractions;
using OrdersMicroservice.Api.Helpers;

namespace OrdersMicroservice.Api.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _orderRepository;
        public OrdersService(IOrdersRepository ordersRepository)
        {
            _orderRepository = ordersRepository;
        }

        public async Task<OrderDto> Archive(int orderId)
        {
            var orderExist = (await _orderRepository.GetById(orderId));
            if (orderExist == null)
                throw new NotFoundException($"Order with id: {orderId} doesn't exist");

            if(orderExist.Status == "Delivered")
            {
                throw new BadRequestException("Order is already delivered");
            }
       
            var data = await _orderRepository.Archive(orderId);
            OrderDto orderDto = data.ToDto();
            orderDto.Customer = await UsersHelper.GetUser(orderDto.CreatorId);
            orderDto.Deliverer = await UsersHelper.GetUser(orderDto.DelivererId);

            return orderDto;
        }

        public async Task<OrderDto> Delete(int id)
        {
            var orderExist = (await _orderRepository.GetById(id));
            if (orderExist == null)
                throw new NotFoundException($"Order with id: {id} doesn't exist");

            var order = await _orderRepository.Delete(id);
            
            return order.ToDto();
        }

        public async Task<OrderDto> GetByID(int id)
        {
            var orderExist = (await _orderRepository.GetById(id));
            if (orderExist == null)
                throw new NotFoundException($"Order with id: {id} doesn't exist");
            var order = await _orderRepository.GetById(id);
            OrderDto orderDto = order.ToDto();
            orderDto.Customer = await UsersHelper.GetUser(orderDto.CreatorId);
            orderDto.Deliverer = await UsersHelper.GetUser(orderDto.DelivererId);

            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> GetOrderByCustomerId(string id)
        {
            var orders = await _orderRepository.GetOrdersByCustomerId(id);
            var ordersDTO = new List<OrderDto>();

            foreach (var item in orders)
            {
                OrderDto orderDto = item.ToDto();
                orderDto.Customer = await UsersHelper.GetUser(orderDto.CreatorId);
                orderDto.Deliverer = await UsersHelper.GetUser(orderDto.DelivererId);

                ordersDTO.Add(orderDto);
            }

            return ordersDTO;
        }

        public async Task<IEnumerable<OrderDto>> GetOrderByDelivererId(string id)
        {
            var orders = await _orderRepository.GetOrdersByDelivererId(id);
            var ordersDTO = new List<OrderDto>();

            foreach (var item in orders)
            {
                OrderDto orderDto = item.ToDto();
                orderDto.Customer = await UsersHelper.GetUser(orderDto.CreatorId);
                orderDto.Deliverer = await UsersHelper.GetUser(orderDto.DelivererId);

                ordersDTO.Add(orderDto);
            }

            return ordersDTO;
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            var ordersDTO = new List<OrderDto>();
            foreach (var item in orders)
            {
                OrderDto orderDto = item.ToDto();
                orderDto.Customer = await UsersHelper.GetUser(orderDto.CreatorId);
                orderDto.Deliverer = await UsersHelper.GetUser(orderDto.DelivererId);

                ordersDTO.Add(orderDto);
            }
            return ordersDTO;
        }

        public async Task<OrderDto> Add(OrderDto OrderDto)
        {
            var data = await _orderRepository.Add(OrderDto.ToEntity());
            OrderDto orderDto1 = data.ToDto();
            orderDto1.Customer = await UsersHelper.GetUser(orderDto1.CreatorId);
            orderDto1.Deliverer = await UsersHelper.GetUser(orderDto1.DelivererId);

            return orderDto1;
        }

        public async Task<OrderDto> Take(int orderId, TakeOrderDto takaOrder)
        {
            var orderExist = (await _orderRepository.GetById(orderId));
            if (orderExist == null)
                throw new NotFoundException($"Order with id: {orderId} doesn't exist");

            if(orderExist.Status != "Waiting")
            {
                throw new BadRequestException($"Orders is in status: {orderExist.Status.ToString()}");
            }

            var orders = await _orderRepository.GetOrdersByDelivererId(takaOrder.DelivererId);

            foreach (var item in orders)
            {
                if(item.Status == "In progress")
                {
                    throw new BadRequestException("Only one order is allow to take");
                }
            }

            var data = await _orderRepository.Take(orderId, takaOrder);
            OrderDto orderDto = data.ToDto();
            orderDto.Customer = await UsersHelper.GetUser(orderDto.CreatorId);
            orderDto.Deliverer = await UsersHelper.GetUser(orderDto.DelivererId);

            return orderDto;
        }
    }
}
