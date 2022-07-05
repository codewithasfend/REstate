using EcommerceSampleApi.Data;
using EcommerceSampleApi.Interfaces;
using EcommerceSampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSampleApi.Services
{
    public class PropertyService : IPropertyService
    {
        private ApiDbContext _dbContext;
        public PropertyService()
        {
            _dbContext = new ApiDbContext();
        }

        public async Task<bool> AddProperty(Property property, string userEmail)
        {

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null) return false;

            property.UserId = user.Id;
            property.IsTrending = false;
            _dbContext.Properties.Add(property);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteProperty(int id,string userEmail)
        {
            var propertyResult = await _dbContext.Properties.FindAsync(id);
            if (propertyResult == null)
            {
                return false;
            }
            else
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
                if (user != null && propertyResult.UserId == user.Id)
                {
                        _dbContext.Properties.Remove(propertyResult);
                        await _dbContext.SaveChangesAsync();
                        return true;
                }

                return false;
            }
        }

        public async Task<(bool IsSuccess, List<Property> Properties)> GetAllProperties(int categoryId)
        {
            var propertyResult = await _dbContext.Properties.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (propertyResult == null)
            {
                return (false, null);
            }

            return (true, propertyResult);
        }

        public async Task<(bool IsSuccess, Property Property)> GetPropertyDetail(int id)
        {
            var propertyResult = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (propertyResult == null)
            {
                return (false, null);
            }

            return (true, propertyResult);
        }

        public async Task<(bool IsSuccess, List<Property> Properties)> GetTrendingProperties()
        {
            var propertyResult = await _dbContext.Properties.Where(x => x.IsTrending).ToListAsync();
            if (propertyResult == null)
            {
                return (false, null);
            }

            return (true, propertyResult);
        }

        public async Task<(bool IsSuccess, List<Property> Properties)> SearchProperty(string propertyAddress)
        {
            var propertyResult = await _dbContext.Properties.Where(x => x.Address.Contains(propertyAddress)).ToListAsync();
            if (propertyResult == null)
            {
                return (false, null);
            }

            return (true, propertyResult);
        }

        public async Task<bool> UpdateProperty(int id, Property property, string userEmail)
        {
            var propertyResult = await _dbContext.Properties.FirstOrDefaultAsync(p => p.Id == id);
            if (propertyResult == null)
            {
                return false;
            }
            else
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
                if (user != null && propertyResult.UserId == user.Id)
                {
                    _dbContext.Properties.Update(property);
                    await _dbContext.SaveChangesAsync();
                    return true;
                   
                }
                return false;
            }
        }
    }
}
