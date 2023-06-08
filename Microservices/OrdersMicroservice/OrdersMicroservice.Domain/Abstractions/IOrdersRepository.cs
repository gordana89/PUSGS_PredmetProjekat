using OrdersMicroservice.Domain.Dtos;
using OrdersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersMicroservice.Domain.Abstractions
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetOrdersByCustomerId(string id);
        Task<IEnumerable<Order>> GetOrdersByDelivererId(string id);
        Task<Order> GetById(int id);
        Task<Order> Add(Order order);
        Task<Order> Take(int orderId, TakeOrderDto takaOrder);
        Task<Order> Archive(int orderId);
        Task<Order> Delete(int id);
    }
}
