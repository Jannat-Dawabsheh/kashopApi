using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Models
{
    [PrimaryKey(nameof(ProductId),nameof(OrderId))]
    public class OrderItem
    {
        public int ProductId { get; set; }
        public Product product { get; set; }
        public int OrderId { get; set; }
        public Order order { get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
