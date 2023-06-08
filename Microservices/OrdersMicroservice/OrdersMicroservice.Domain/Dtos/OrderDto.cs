using System.Collections.Generic;

namespace OrdersMicroservice.Domain.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public float Price { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string DelivererId { get; set; }
        public string CreatorId { get; set; }
        public string Time { get; set; }
        public UserDto Customer { get; set; }
        public UserDto Deliverer { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
