using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMicroservice.Domain.Dtos
{
    public class TakeOrderDto
    {
        public string DelivererId { get; set; }
        public string TimeForDelivery { get; set; }
    }
}
