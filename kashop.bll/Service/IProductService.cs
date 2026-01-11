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
        public Task<ProductResponse> CreateProduct(ProductRequest request);
        public Task<List<ProductResponse>> GetAllProductsAsyncForAdmin();
        public Task<List<ProductUserResponse>> GetAllProductsAsyncForUser(string lang = "en");
        public Task<ProductUserDetails> GetAllProductsDetailsAsyncForUser(int id, string lang = "en");

    } 
}
