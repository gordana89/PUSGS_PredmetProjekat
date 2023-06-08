using System.Collections.Generic;

namespace OrdersMicroservice.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public float Price { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string DelivererId { get; set; }
        public string CreatorId { get; set; }
        public string TimeForDelivery { get; set; }
        public virtual ICollection<ProductOrder> Products { get; set; }

    }
}
