using EcommerceSampleApi.Data;
using EcommerceSampleApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public PropertiesController()
        {
            _dbContext = new ApiDbContext();
        }

        [HttpGet("PropertyList")]
        public IActionResult GetProperties(int categoryId)
        {
            var propertyResult = _dbContext.Properties.Where(x => x.CategoryId == categoryId);
            if (propertyResult == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult);
        }

        [HttpGet("PropertyDetail")]
        public IActionResult GetPropertyDetail(int id)
        {
            var propertyResult = _dbContext.Properties.FirstOrDefault(x => x.Id == id);
            if (propertyResult == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult);
        }

        [HttpGet("TrendingProperty")]
        public IActionResult GetTrendingProperties()
        {
            var propertyResult = _dbContext.Properties.Where(x => x.IsTrending);
            if (propertyResult == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult);
        }



        [HttpGet("[action]")]
        public IActionResult Search(string location)
        {
            var propertyResult = _dbContext.Properties.Where(x => x.Address.Contains(location));
            if (propertyResult == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Property property)
        {
            if (property == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
                if (user == null) return NotFound();

                property.UserId = user.Id;
                property.IsTrending = false;
                _dbContext.Properties.Add(property);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Property property)
        {
            var propertyResult = _dbContext.Properties.FirstOrDefault(p => p.Id == id);
            if (propertyResult == null)
            {
                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
                if (user != null)
                {
                    if (propertyResult.UserId == user.Id)
                    {
                        propertyResult.Name = property.Name;
                        propertyResult.Detail = property.Detail;
                        propertyResult.Price = property.Price;
                        propertyResult.Address = property.Address;
                        _dbContext.SaveChanges();
                        return Ok("Record updated successfully");
                    }
                    else
                        return BadRequest();
                }
                return NotFound();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var propertyResult = _dbContext.Properties.Find(id);
            if (propertyResult == null)
            {
                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
                if (user != null)
                {
                    if (propertyResult.UserId == user.Id)
                    {
                        _dbContext.Properties.Remove(propertyResult);
                        _dbContext.SaveChanges();
                        return Ok("Record deleted successfully");
                    }
                    else
                        return BadRequest();
                }

                return NotFound();
            }
        }

    }
}
