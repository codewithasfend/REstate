using EcommerceSampleApi.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private ApiDbContext _dbContext;
        public CategoriesController()
        {
            _dbContext = new ApiDbContext();
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }

    }
}
