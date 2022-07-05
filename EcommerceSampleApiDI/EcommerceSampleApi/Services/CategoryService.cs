using EcommerceSampleApi.Data;
using EcommerceSampleApi.Interfaces;
using EcommerceSampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSampleApi.Services
{
    public class CategoryService : ICategoryService
    {
        private ApiDbContext _dbContext;
        public CategoryService()
        {
            _dbContext = new ApiDbContext();
        }

        public Task<List<Category>> Getcategories()
        {
            return _dbContext.Categories.ToListAsync();
        }
    }
}
