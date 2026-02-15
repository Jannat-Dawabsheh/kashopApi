using kashop.dal.DTO.Response;
using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product request);
        Task<List<Product>> GetAllAsync();
        Task<bool>DecreaseQuantityAsync(List<(int productId, int quantity)> items);
        Task<Product?> FindByIdAsync(int id);
        IQueryable<Product> Query();
        Task<Product?> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
