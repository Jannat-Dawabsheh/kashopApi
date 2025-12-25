using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category Request);
        Task<Category?> FindByIdAsync(int id);
        Task<Category?> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
