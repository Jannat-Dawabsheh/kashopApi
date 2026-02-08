using kashop.dal.DTO.Response;
using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrdersAsync(OrderStatusEnum status);
        Task<BaseResponse>UpdateOrderStatusAsync(int orderId, OrderStatusEnum newStatus);
        Task<Order?> GetOrderByIdAsync(int orderId);
        
    }
}
