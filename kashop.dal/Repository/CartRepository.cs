using Azure.Core;
using kashop.dal.Data;
using kashop.dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> CreateAsync(Cart request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<Cart>> GetUserCartAsync(string userId)
        {
           return await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ThenInclude(c=>c.Translations)
                .ToListAsync();
        }

        public async Task<Cart?>GetCartItemAsync(string userId,int productId)
        {
            return await _context.Carts.Include(c=>c.Product)
              .FirstOrDefaultAsync(c=>c.UserId==userId && c.ProductId == productId);
        }

        

        public async Task<Cart> UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;

        }

        public async Task DeleteAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
           

        }

        public async Task ClearCartAsync(string userId)
        {
            var items=await _context.Carts.Where(c=>c.UserId==userId).ToListAsync();
            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
