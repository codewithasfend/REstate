using EcommerceSampleApi.Data;
using EcommerceSampleApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_categoryService.Getcategories());
        }

    }
}
