
using OrdersMicroservice.Domain.Dtos;
using OrdersMicroservice.Domain.Entities;
using System.Collections.Generic;

namespace OrdersMicroservice.Api.Extensions
{
    public static class ModelsExtensions
    {
        
        public static Product ToEntity(this ProductDto productDto)
        {
            Product product = new Product();
            product.Id = productDto.Id;
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Components = productDto.Components;
            return product;
        }
        public static ProductDto ToDto(this Product product)
        {
            ProductDto productDTO = new ProductDto();
            productDTO.Id = product.Id;
            productDTO.Name = product.Name;
            productDTO.Price = product.Price;
            productDTO.Components = product.Components;
            return productDTO;
        }
        public static Order ToEntity(this OrderDto orderDto)
        {
            Order order = new Order();
            order.Id = orderDto.Id;
            order.Price = orderDto.Price;
            order.Status = orderDto.Status;
            order.Address = orderDto.Address;
            order.Comment = orderDto.Comment;
            order.CreatorId = orderDto.CreatorId;
            order.DelivererId = orderDto.DelivererId;
            order.Products = new List<ProductOrder>();
            foreach (var item in orderDto.Products)
            {
                order.Products.Add(new ProductOrder()
                {
                    OrderId = orderDto.Id,
                    ProductId = item.Id
                });
            }

            return order;
        }
        public static OrderDto ToDto(this Order order)
        {
            OrderDto orderDto = new OrderDto();
            orderDto.Id = order.Id;
            orderDto.Price = order.Price;
            orderDto.Status = order.Status;
            orderDto.Address = order.Address;
            orderDto.Comment = order.Comment;
            orderDto.CreatorId = order.CreatorId;
            orderDto.Time = order.TimeForDelivery;
            orderDto.DelivererId = order.DelivererId;
            orderDto.Products = new List<ProductDto>();
            if (order.Products != null)
            {
                foreach (var item in order.Products)
                {
                    orderDto.Products.Add(new ProductDto()
                    {
                        Id = item.Product.Id,
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        Components = item.Product.Components
                    });
                }
            }
            return orderDto;
        }

    }
}
