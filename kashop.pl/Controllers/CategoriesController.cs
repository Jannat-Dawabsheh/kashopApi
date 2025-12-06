using Azure;
using kashop.bll.Service;
using kashop.dal.Data;
using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using kashop.dal.Models;
using kashop.dal.Repository;
using kashop.pl.Resourses;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace kashop.pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IStringLocalizer<SharedResource>localizer,ICategoryService categoryService)
        {
          
            _localizer = localizer;
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult index() {

            var response = _categoryService.GetAllCategories();
            return Ok(new { message = _localizer["Success"].Value,response });

        }


        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
           
            var response=_categoryService.CreateCategory(request);
            
            return Ok(new { message = _localizer["Success"].Value});

        }


    }
}
