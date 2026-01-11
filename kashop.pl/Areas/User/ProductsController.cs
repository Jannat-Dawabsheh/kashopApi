using kashop.bll.Service;
using kashop.pl.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace kashop.pl.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(IProductService product, IStringLocalizer<SharedResource> localizer)
        {
            _product = product;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> IndexAsync([FromQuery] string lang = "en")
        {
            var response = await _product.GetAllProductsAsyncForUser(lang);
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> IndexAsync([FromRoute]int id, string lang = "en")
        {
            var response = await _product.GetAllProductsDetailsAsyncForUser(id,lang);
            return Ok(new { message = _localizer["Success"].Value, response });
        }
    }
}
