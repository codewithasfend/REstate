using EcommerceSampleApi.Models;

namespace EcommerceSampleApi.Interfaces
{
    public interface IUserService
    {
        Task<(bool IsSuccess,User user)> LoginUser(UserLogin userLogin);    
        Task<bool> RegisterUser(User user);
    }
}
