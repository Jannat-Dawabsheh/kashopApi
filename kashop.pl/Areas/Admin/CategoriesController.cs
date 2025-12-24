using kashop.bll.Service;
using kashop.dal.DTO.Request;
using kashop.dal.Models;
using kashop.pl.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace kashop.pl.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _category;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ICategoryService category, IStringLocalizer<SharedResource> localizer)
        {
            _category = category;
            _localizer = localizer;
        }

       

        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var createdBy=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = _category.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
