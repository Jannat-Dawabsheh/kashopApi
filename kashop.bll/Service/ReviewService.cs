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
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<BaseResponse> AddReviewAsync(string userId,int productId, CreateReviewRequest request)
        {
            var hasDelivered = await _orderRepository.HasUserDeliveredOrderForProduct(userId, productId);
            if (!hasDelivered)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "you can only review product you have received"
                };
            }

            var alreadyReview=await _reviewRepository.HasUserReviewProduct(userId, productId);
            if (alreadyReview)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "can't add review"
                };
            }

            var review = request.Adapt<Review>();
            review.UserId= userId;
            review.ProductId= productId;
            await _reviewRepository.CreateAsync(review);

            return new BaseResponse
            {
                Success = true,
                Message = "review added successfully"
            };
        }
    }
}
