using kashop.bll.Service;
using kashop.dal.DTO.Request;
using kashop.pl.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace kashop.pl.Areas.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productServices;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(IProductService productServices, IStringLocalizer<SharedResource> localizer)
        {
            _productServices = productServices;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            var response = await _productServices.GetAllProductsAsyncForAdmin();
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]ProductRequest request)
        {
            
            var response=await _productServices.CreateProduct(request);
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await _productServices.DeleteProductAsync(id);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
