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
    [Authorize(Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResource> localizer)
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }


        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            var response = await _categoryService.GetAllCategoriesAsyncForAdmin();
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody]CategoryRequest request)
        {
            var createdBy=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _categoryService.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody]CategoryRequest request)
        {
            var result = await _categoryService.UpdateCategoryAsync(id,request);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            var result=await _categoryService.DeleteCategoryAsync(id);
            if (!result.Success)
            {
                if(result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("toggle-status/{id}")]
        public async Task<IActionResult> DeleToggleStatusteCategory([FromRoute] int id)
        {
            var result = await _categoryService.ToggleStatus(id);
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
