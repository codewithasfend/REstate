using EcommerceSampleApi.Models;

namespace EcommerceSampleApi.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> Getcategories();
    }
}
