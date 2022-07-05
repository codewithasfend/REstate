using EcommerceSampleApi.Data;
using EcommerceSampleApi.Interfaces;
using EcommerceSampleApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _config;
        public UsersController(IConfiguration config , IUserService userService)
        {
            _config = config;
            _userService = userService; 
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var result = await _userService.LoginUser(userLogin);
            if (result.IsSuccess)
            {
                var token = GenerateToken(result.user);
                return Ok(token);
            }
            return NotFound("User not found");

        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email , user.Email),

            };

            var token = new JwtSecurityToken(
                _config["JWT:Issuer"], 
                _config["JWT:Audience"], 
                claims, 
                expires: DateTime.Now.AddMinutes(60), 
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _userService.RegisterUser(user);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
            return Ok("User Created Successfully");

        }
    }
}
