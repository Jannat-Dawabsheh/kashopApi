using kashop.dal.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kashop.pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

       
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
