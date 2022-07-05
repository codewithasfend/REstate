using EcommerceSampleApi.Data;
using EcommerceSampleApi.Interfaces;
using EcommerceSampleApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private IPropertyService _propertyService;
        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("PropertyList")]
        public async Task<IActionResult> GetProperties(int categoryId)
        {
            var propertyResult = await _propertyService.GetAllProperties(categoryId);
            if (!propertyResult.IsSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult.Properties);
        }

        [HttpGet("PropertyDetail")]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var propertyResult = await _propertyService.GetPropertyDetail(id);
            if (!propertyResult.IsSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult.Property);
        }

        [HttpGet("TrendingProperty")]
        public async Task<IActionResult> GetTrendingProperties()
        {
            var propertyResult = await _propertyService.GetTrendingProperties();
            if (!propertyResult.IsSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult.Properties);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string location)
        {
            var propertyResult = await _propertyService.SearchProperty(location);
            if (!propertyResult.IsSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(propertyResult.Properties);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Property property)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (property == null && string.IsNullOrEmpty(userEmail))
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            else
            {
                var user = await _propertyService.AddProperty(property, userEmail);

                if (!user) return NotFound();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Property property)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var propertyResult = await _propertyService.UpdateProperty(id, property, userEmail);
            if (!propertyResult)
            {
                return NotFound();
            }
            else
            {
                if (propertyResult == true)
                {
                    return Ok("Record updated successfully...");

                }
                return NotFound();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var propertyResult = await _propertyService.DeleteProperty(id, userEmail);
            if (!propertyResult)
            {
                return NotFound();
            }
            else
            {
                if (propertyResult == true)
                {
                    return Ok("Record deleted successfully");
                }
                return NotFound();
            }
        }

    }
}
