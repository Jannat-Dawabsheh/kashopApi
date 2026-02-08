using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public interface IOrderItemRepository
    {
        Task CreateRangeAsync(List<OrderItem> orderItems);
    }
}
