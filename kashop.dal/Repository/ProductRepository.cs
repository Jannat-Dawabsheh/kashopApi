using kashop.dal.Data;
using kashop.dal.DTO.Response;
using kashop.dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(c => c.Translations).Include(c => c.User).ToListAsync();
        }
        public async Task<Product> AddAsync(Product request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
           
        }

        public async Task<Product?> FindByIdAsync(int id)
        {
            return await _context.Products.Include(c => c.Translations)
                .Include(c=>c.SubImages)
                .Include(c=>c.Reviews)
                .ThenInclude(r=>r.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Product> Query()
        {
            return _context.Products.Include(c => c.Translations).AsQueryable();
        }

        public async Task<bool> DecreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds=items.Select(p=>p.productId).ToList();
            var products=await _context.Products.Where(p=>productIds.Contains(p.Id)).ToListAsync();

            foreach (var product in products) {
                var item = items.FirstOrDefault(p=>p.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                    return false;
                }
                product.Quantity -= item.quantity;

            }

           
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
