using EcommerceSampleApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
        [Authorize]

        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }

    }
}
