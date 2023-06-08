using System.Collections.Generic;

namespace OrdersMicroservice.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Components { get; set; }
        public virtual ICollection<ProductOrder> Orders { get; set; }
    }
}
