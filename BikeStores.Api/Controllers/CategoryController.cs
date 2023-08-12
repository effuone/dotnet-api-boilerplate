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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            _logger.LogInformation($"Retrieved {categories.Count()} categories");
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category == null)
            {
                _logger.LogInformation($"Category with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved category with ID {id}");
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var newCategoryId = await _categoryRepository.CreateAsync(category);
            _logger.LogInformation($"Created category with ID {newCategoryId}");
            return CreatedAtAction(nameof(GetCategory), new { id = newCategoryId }, _mapper.Map<CategoryDto>(category));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _categoryRepository.GetAsync(id);
            if (existingCategory == null)
            {
                _logger.LogInformation($"Category with ID {id} not found");
                return NotFound();
            }

            var updatedCategory = _mapper.Map<Category>(updateCategoryDto);
            await _categoryRepository.UpdateAsync(id, updatedCategory);
            _logger.LogInformation($"Updated category with ID {id}");
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryRepository.GetAsync(id);
            if (existingCategory == null)
            {
                _logger.LogInformation($"Category with ID {id} not found");
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(id);
            _logger.LogInformation($"Deleted category with ID {id}");
            return NoContent();
        }
    }
}
