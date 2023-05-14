using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeStores.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepositiory;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository categoryRepositiory, ILogger<CategoryController> logger)
        {
            _categoryRepositiory = categoryRepositiory;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepositiory.GetAllAsync();
            _logger.LogInformation($"Retrieved {categories.Count()} categories");
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepositiory.GetAsync(id);

            if (category == null)
            {
                _logger.LogInformation($"Category with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved category with ID {id}");
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var newCategoryId = await _categoryRepositiory.CreateAsync(category);

            _logger.LogInformation($"Created category with ID {newCategoryId}");
            return CreatedAtAction(nameof(GetCategory), new { id = newCategoryId }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            await _categoryRepositiory.UpdateAsync(id, category);

            _logger.LogInformation($"Updated category with ID {id}");
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepositiory.DeleteAsync(id);

            _logger.LogInformation($"Deleted category with ID {id}");
            return NoContent();
        }
    }
}