using EcommerceSampleApi.Models;

namespace EcommerceSampleApi.Interfaces
{
    public interface IPropertyService
    {
        Task<bool> AddProperty(Property property,string userEmail);
        Task<bool> UpdateProperty(int id, Property property,string userEmail);
        Task<bool> DeleteProperty(int id, string userEmail);
        Task<(bool IsSuccess, List<Property> Properties)> GetAllProperties(int categoryId);
        Task<(bool IsSuccess,Property Property)> GetPropertyDetail(int id);
        Task<(bool IsSuccess, List<Property> Properties)> GetTrendingProperties();
        Task<(bool IsSuccess, List<Property> Properties)> SearchProperty(string propertyAddress);
    }
}
