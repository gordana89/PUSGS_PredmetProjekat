using OrdersMicroservice.Domain.Abstractions;
using OrdersMicroservice.Domain.Context;
using OrdersMicroservice.Domain.Dtos;
using OrdersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersMicroservice.Api.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _dataContext;
        public OrdersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Order> Archive(int orderId)
        {
            var order = await _dataContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = "Delivered";
            }
            await _dataContext.Products.Include(w => w.Orders).ToListAsync();
            _dataContext.SaveChanges();
            return order;
        }

        public async Task<Order> Delete(int id)
        {
            var order = await _dataContext.Orders.FindAsync(id);
            if(order != null)
            {
                _dataContext.Remove(order);
                await _dataContext.SaveChangesAsync();
            }
            await _dataContext.Orders.Include(w => w.Products).ToListAsync();
            return order;
        }

        public async Task<Order> GetById(int id)
        {
            await _dataContext.Products.Include(w => w.Orders).ToListAsync();
            Order order = await _dataContext.Orders.FindAsync(id);
            
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            await _dataContext.Products.Include(w => w.Orders).ToListAsync();
            var orders = await _dataContext.Orders.Include(w => w.Products).ToListAsync();
            
            return orders;
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(string id)
        {

            await _dataContext.Products.Include(w => w.Orders).ToListAsync();

            return await _dataContext.Orders.Include(i => i.Products).Where(w => w.CreatorId == id).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDelivererId(string id)
        {
            await _dataContext.Products.Include(w => w.Orders).ToListAsync();

            return await _dataContext.Orders.Where(w => w.DelivererId == id).ToListAsync();

        }

        public async Task<Order> Add(Order order)
        {
            List<ProductOrder> orderProducts = new List<ProductOrder>(order.Products);
            order.Products.Clear();
            order.Status = "Waiting";
            var data = await _dataContext.AddAsync(order);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                string e = ex.Message;
                throw;
            }

            foreach (var item in orderProducts)
            {
                item.OrderId = data.Entity.Id;
                await _dataContext.ProductsOrders.AddAsync(item);
            }
            await _dataContext.SaveChangesAsync();
            await _dataContext.Products.Include(w => w.Orders).ToListAsync();
            
            return data.Entity;
        }

        public async Task<Order> Take(int orderId, TakeOrderDto takaOrder)
        {
            var order = await _dataContext.Orders.FindAsync(orderId);
            if(order != null)
            {
                order.Status = "In progress";
                order.DelivererId = takaOrder.DelivererId;
                order.TimeForDelivery = takaOrder.TimeForDelivery;
            }
            await _dataContext.Products.Include(w => w.Orders).ToListAsync();
            _dataContext.SaveChanges();
            return order;
        }
    }
}
