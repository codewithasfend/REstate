using EcommerceSampleApi.Data;
using EcommerceSampleApi.Interfaces;
using EcommerceSampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSampleApi.Services
{
    public class UserService : IUserService
    {
        private ApiDbContext _dbContext;

        public UserService()
        {
            _dbContext = new ApiDbContext();
        }
        public async Task<(bool IsSuccess,User user)> LoginUser(UserLogin userLogin)
        {
            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
            if (currentUser != null)
            {
                return (true,currentUser);
            }
            return (false, null);
        }

        public async Task<bool> RegisterUser(User user)
        {
            var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userExists != null)
            {
                return false;
            }
            var userCreated = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
