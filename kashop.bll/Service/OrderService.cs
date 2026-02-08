using kashop.dal.DTO.Response;
using kashop.dal.Models;
using kashop.dal.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<List<OrderResponse>> GetOrdersAsync(OrderStatusEnum status)
        {
           var orders= await _orderRepository.GetOrderByStatusAsync(status);
            return orders.Adapt<List<OrderResponse>>();
        }

        public async Task<BaseResponse> UpdateOrderStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            order.OrderStatus=newStatus;

            if (newStatus == OrderStatusEnum.Delivered)
            {
                order.PaymentStatus = PaymentStatusEnum.Paid;
            }
            else if(newStatus == OrderStatusEnum.Cancelled)
            {
                if(newStatus == OrderStatusEnum.Shipped)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Can't cancelled this order"
                    };
                }
            }

            await _orderRepository.UpdateAsync(order);

            return new BaseResponse
            {
                Success = true,
                Message = "order status updated successfully"
            };
        }
    }
}
