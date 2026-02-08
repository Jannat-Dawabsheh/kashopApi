using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart request);
        Task<List<Cart>> GetUserCartAsync(string userId);
        Task<Cart?> GetCartItemAsync(string userId, int productId);
        Task<Cart> UpdateAsync(Cart cart);
        Task DeleteAsync(Cart cart);
        Task ClearCartAsync(string userId);
    }
}
