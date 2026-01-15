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
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(IProductRepository productRepository,ICartRepository cartRepository) {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        public async Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if(product is null)
            {
                return new BaseResponse
                {
                   Success = false,
                   Message="Product not found"
                };
            }

            if (product.Quantity < request.Count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Not enough stock"
                };
            }
            var cartItem=await _cartRepository.GetCartItemAsync(userId,request.ProductId);
            if(cartItem is not null)
            {
                cartItem.Count += request.Count;
                await _cartRepository.UpdateAsync(cartItem);
            }
            else
            {
                var cart = request.Adapt<Cart>();
                cart.UserId = userId;
                await _cartRepository.CreateAsync(cart);


            }


            return new BaseResponse
            {
                Success = true,
                Message = "Product added successfully"
            };

        }

        public async Task<CartSummaryResponse> GetUserCartAsync(string userId, string lang = "en")
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            

            var Items = cartItems.Select(c => new CartResponse
            {
                ProductId = c.ProductId,
                ProductName = c.Product.Translations.FirstOrDefault(t => t.Language == lang).Name,
                Count = c.Count,
                Price=c.Product.Price
            }).ToList();

            return new CartSummaryResponse
            {
                Items = Items,
            };
        }

        public async Task<BaseResponse>ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return new BaseResponse
            {
                Success = true,
                Message = "Cart cleared successfully"

            };
        }
    }
}
