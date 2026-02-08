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

            var cartItem = await _cartRepository.GetCartItemAsync(userId, request.ProductId);

            var existingCount=cartItem?.Count ?? 0;

            if (product.Quantity < (existingCount + request.Count))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Not enough stock"
                };
            }
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

        public async Task<BaseResponse> UpdateQuantityAsync(string userId,int productId, int count)
        {
            var cartItems = await _cartRepository.GetCartItemAsync(userId,productId);
            var product=await _productRepository.FindByIdAsync(productId);

            if (count == 0)
            {
                await _cartRepository.DeleteAsync(cartItems);
                return new BaseResponse
                {
                    Success = true,
                    Message = "item removed from cart successfully"

                };
            }
            
            if (product.Quantity < count)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "not enough stock"

                };
            }

            cartItems.Count = count;
            await _cartRepository.UpdateAsync(cartItems);

            return new BaseResponse
            {
                Success = true,
                Message = "Quantity Updated successfully"

            };
        }
        public async Task<BaseResponse>RemoveFromCartAsync(string userId,int productId)
        {
            var cartItem = await _cartRepository.GetCartItemAsync(userId, productId);

            if(cartItem is null)
            {
                return new BaseResponse
                {
                    Success= false,
                    Message="cart item not found"
                };
            }

            await _cartRepository.DeleteAsync(cartItem);
            return new BaseResponse { 
            
                Success = true,
                Message = "item removed from cart"
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
