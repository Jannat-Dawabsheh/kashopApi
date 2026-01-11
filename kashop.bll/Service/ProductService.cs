using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using kashop.dal.Models;
using kashop.dal.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileServices _fileServices;

        public ProductService(IProductRepository productRepository,IFileServices fileServices) {
            _productRepository = productRepository;
            _fileServices = fileServices;
        }

        public async Task<List<ProductResponse>> GetAllProductsAsyncForAdmin()
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.Adapt<List<ProductResponse>>();
            return response;
        }
        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        
        {
           
            var product = request.Adapt<Product>();
            if (request.MainImage!=null) {
              var imagePath=await _fileServices.UploadAsync(request.MainImage);
              product.MainImage = imagePath;
            }
            if (request.SubImages != null)
            {
                product.SubImages = new List<ProductImage>();
                foreach (var file in request.SubImages)
                {
                    var imagePath = await _fileServices.UploadAsync(file);
                    product.SubImages.Add(new ProductImage { ImageName = imagePath });
                }
            }
            
            await _productRepository.AddAsync(product);
            return product.Adapt<ProductResponse>();
        }

        public async Task<List<ProductUserResponse>> GetAllProductsAsyncForUser(string lang = "en")
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<ProductUserResponse>>();
            return response;
        }

        public async Task<ProductUserDetails> GetAllProductsDetailsAsyncForUser(int id,string lang = "en")
        {
            var product = await _productRepository.FindByIdAsync(id);
            var response = product.BuildAdapter().AddParameters("lang", lang).AdaptToType<ProductUserDetails>();
            return response;
        }
    }
}
