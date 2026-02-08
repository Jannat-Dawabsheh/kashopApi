using kashop.bll.Service;
using kashop.dal.DTO.Request;
using kashop.pl.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace kashop.pl.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IReviewService _reviewService;

        public ProductsController(IProductService product, IStringLocalizer<SharedResource> localizer
            ,IReviewService reviewService)
        {
            _product = product;
            _localizer = localizer;
            _reviewService = reviewService;
        }

        [HttpGet("")]
        public async Task<IActionResult> IndexAsync([FromQuery] string lang = "en", [FromQuery]int page = 1, [FromQuery]int limit=3
            , [FromQuery] string? search = null, [FromQuery] int? categoryId = null,
            [FromQuery]decimal? minPrice = null,[FromQuery]decimal? maxPrice=null,
              [FromQuery] string? sortBy = null, [FromQuery] bool asc = true)
        {
            var response = await _product.GetAllProductsAsyncForUser(lang,page,limit,search,categoryId,minPrice,maxPrice, sortBy,asc);
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> IndexAsync([FromRoute]int id, string lang = "en")
        {
            var response = await _product.GetAllProductsDetailsAsyncForUser(id,lang);
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpPost("{productId}/reviews")]
        public async Task<IActionResult> AddReview([FromRoute] int productId, [FromBody] CreateReviewRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response=await _reviewService.AddReviewAsync(userId,productId,request);
            if(!response.Success)return BadRequest(new {message=response.Message});
            return Ok(new { message = response.Message });
        }
    }
}
