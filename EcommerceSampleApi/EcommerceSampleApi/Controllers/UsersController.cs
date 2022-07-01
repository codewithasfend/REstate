using EcommerceSampleApi.Data;
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
        private ApiDbContext _dbContext;
        private IConfiguration _config;
        public UsersController(IConfiguration config)
        {
            _dbContext = new ApiDbContext();
            _config = config;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
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
                new Claim(ClaimTypes.NameIdentifier , user.Name),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.PrimarySid , user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                _config["JWT:Issuer"], 
                _config["JWT:Audience"], 
                claims, 
                expires: DateTime.Now.AddMinutes(15), 
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLogin userLogin)
        {
            var curretUser = _dbContext.Users.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
            if (curretUser != null)
            {
                return curretUser;
            }
            return null;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] User user)
        {
            var userExists = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var userCreated = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Ok("User Created Successfully");

        }
    }
}
