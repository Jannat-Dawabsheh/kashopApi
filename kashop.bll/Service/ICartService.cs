using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface ICartService
    {
        Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request);
        Task<CartSummaryResponse> GetUserCartAsync(string userId, string lang = "en");
        Task<BaseResponse> UpdateQuantityAsync(string userId, int productId, int count);
        Task<BaseResponse> RemoveFromCartAsync(string userId, int productId);

        Task<BaseResponse> ClearCartAsync(string userId);
    }
}
