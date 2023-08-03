using AutoMapper;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Dtos;
using BikeStores.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BikeStores.Api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandController> _logger;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, ILogger<BrandController> logger, IMapper mapper)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [SwaggerResponse(200, "Success")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            _logger.LogInformation($"Retrieved {brands.Count()} brands");
            return Ok(_mapper.Map<IEnumerable<BrandDto>>(brands));
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not found")]        
        public async Task<IActionResult> GetBrandAsync(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            if (brand == null)
            {
                _logger.LogInformation($"Brand with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved brand with ID {id}");
            return Ok(_mapper.Map<BrandDto>(brand));
        }

        [HttpPost]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(409, "Already existing")]
        [SwaggerResponse(400, "Validation errors occured")]
        public async Task<IActionResult> CreateBrandAsync([FromBody] CreateBrandDto createBrandDto)
        {
            var existingBrand = await _brandRepository.GetByNameAsync(createBrandDto.BrandName);
            if (existingBrand != null)
            {
                string loggedErrorMessage = $"Brand with name '{createBrandDto.BrandName}' already exists";
                _logger.LogInformation(loggedErrorMessage);
                return Conflict(loggedErrorMessage);
            }
            var brand = _mapper.Map<Brand>(createBrandDto);
            var newBrandId = await _brandRepository.CreateAsync(brand);
            _logger.LogInformation($"Created brand with ID {newBrandId}");

            return CreatedAtAction(nameof(GetBrandAsync), new { id = newBrandId }, _mapper.Map<BrandDto>(brand));
        }

        [HttpPut("{id}")]
        [SwaggerResponse(204, "No content")]
        [SwaggerResponse(404, "Not found")]
        [SwaggerResponse(400, "Validation errors occured")]
        public async Task<IActionResult> UpdateBrandAsync(int id, [FromBody] UpdateBrandDto updateBrandDto)
        {
            var existingBrand = await _brandRepository.GetAsync(id);
            if (existingBrand == null)
            {
                _logger.LogInformation($"Brand with ID {id} not found");
                return NotFound();
            }

            var newBrand = _mapper.Map<Brand>(updateBrandDto);
            await _brandRepository.UpdateAsync(id, newBrand);
            _logger.LogInformation($"Updated brand with ID {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "No content")]
        [SwaggerResponse(404, "Not found")]
        public async Task<IActionResult> DeleteBrandAsync(int id)
        {
            var existingBrand = await _brandRepository.GetAsync(id);
            if (existingBrand == null)
            {
                _logger.LogInformation($"Brand with ID {id} not found");
                return NotFound();
            }
            await _brandRepository.DeleteAsync(id);

            _logger.LogInformation($"Deleted brand with ID {id}");
            return NoContent();
        }
    }
}
