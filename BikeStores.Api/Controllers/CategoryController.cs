using AutoMapper;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Dtos;
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
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepositiory, ILogger<CategoryController> logger, IMapper mapper)
        {
            _categoryRepositiory = categoryRepositiory ?? throw new ArgumentNullException(nameof(categoryRepositiory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepositiory.GetAllAsync();
            _logger.LogInformation($"Retrieved {categories.Count()} categories");
            return Ok(_mapper.Map<IEnumerable<BrandDto>>(categories));
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
            return Ok(_mapper.Map<BrandDto>(category));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var newCategoryId = await _categoryRepositiory.CreateAsync(category);
            _logger.LogInformation($"Created category with ID {newCategoryId}");
            return CreatedAtAction(nameof(GetCategory), new { id = newCategoryId }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            await _categoryRepositiory.UpdateAsync(id, category);
            _logger.LogInformation($"Updated category with ID {id}");
            return NoContent();
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