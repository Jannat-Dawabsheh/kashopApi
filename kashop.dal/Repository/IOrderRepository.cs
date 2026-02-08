using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order request);
        Task<Order> GetBySessionIdAsync(string sessionId);
        Task<Order> UpdateAsync(Order order);
        Task<List<Order>> GetOrderByStatusAsync(OrderStatusEnum status);
        Task<Order?>GetOrderByIdAsync(int orderId);

        Task<bool> HasUserDeliveredOrderForProduct(string userId, int ProductId);
    }
}
