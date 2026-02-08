using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface ICheckoutService
    {
         Task<CheckoutResponse> ProcessPaymentAsync(CheckoutRequest request, string userId);
        Task<CheckoutResponse> HandelSuccessAsync(string sessionId);
    }
}
