using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface IProductService
    {
         Task<ProductResponse> CreateProduct(ProductRequest request);
         Task<List<ProductResponse>> GetAllProductsAsyncForAdmin();
        Task<PaginationResponse<ProductUserResponse>> GetAllProductsAsyncForUser(string lang = "en", int page = 1, int limit = 3
           , string? search = null
           , int? categoryId = null
           , decimal? minPrice = null
           , decimal? maxPrice = null
           , string? sortBy = null
           , bool asc = true

           );
         Task<ProductUserDetails> GetAllProductsDetailsAsyncForUser(int id, string lang = "en");
        Task<BaseResponse> UpdateProductAsync(int id, UpdateProductRequest request);
         Task<BaseResponse> DeleteProductAsync(int id);
    } 
}
