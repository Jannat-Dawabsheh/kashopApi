using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Request
{
    public class UpdateOrderStatusRequest
    {
        public OrderStatusEnum Status { get; set; }
    }
}
