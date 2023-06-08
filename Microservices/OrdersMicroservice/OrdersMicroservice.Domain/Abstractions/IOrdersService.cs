using OrdersMicroservice.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersMicroservice.Domain.Abstractions
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDto>> GetAll();
        Task<IEnumerable<OrderDto>> GetOrderByCustomerId(string id);
        Task<IEnumerable<OrderDto>> GetOrderByDelivererId(string id);
        Task<OrderDto> GetByID(int id);
        Task<OrderDto> Add(OrderDto orderDto);
        Task<OrderDto> Take(int orderId, TakeOrderDto takaOrder);
        Task<OrderDto> Archive(int orderId);
        Task<OrderDto> Delete(int id);
    }
}
