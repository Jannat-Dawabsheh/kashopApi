using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsyncForAdmin();
        Task<List<CategoryUserResponse>> GetAllCategoriesAsyncForUser(string lang = "en");
        Task<CategoryResponse> CreateCategory(CategoryRequest Request);
        Task<BaseResponse> DeleteCategoryAsync(int id);
        Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request);
        Task<BaseResponse> ToggleStatus(int id);
    }
}
