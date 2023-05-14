using BikeStores.Application.Interfaces;
using BikeStores.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeStores.Api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandRepository brandRepository, ILogger<BrandController> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            _logger.LogInformation($"Retrieved {brands.Count()} brands");
            return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _brandRepository.GetAsync(id);

            if (brand == null)
            {
                _logger.LogInformation($"Brand with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved brand with ID {id}");
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            var newBrandId = await _brandRepository.CreateAsync(brand);

            _logger.LogInformation($"Created brand with ID {newBrandId}");
            return CreatedAtAction(nameof(GetBrand), new { id = newBrandId }, brand);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] Brand brand)
        {
            await _brandRepository.UpdateAsync(id, brand);

            _logger.LogInformation($"Updated brand with ID {id}");
            return Ok(brand);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandRepository.DeleteAsync(id);

            _logger.LogInformation($"Deleted brand with ID {id}");
            return NoContent();
        }
    }
}