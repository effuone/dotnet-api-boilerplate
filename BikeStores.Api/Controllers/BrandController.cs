using AutoMapper;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Dtos;
using BikeStores.Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace BikeStores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            _logger.LogInformation($"Retrieved {brands.Count()} brands");
            return Ok(_mapper.Map<IEnumerable<BrandDto>>(brands));
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
            return Ok(_mapper.Map<BrandDto>(brand));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto createBrandDto)
        {
            var brand = _mapper.Map<Brand>(createBrandDto);
            var newBrandId = await _brandRepository.CreateAsync(brand);
            _logger.LogInformation($"Created brand with ID {newBrandId}");

            return CreatedAtAction(nameof(GetBrand), new { id = newBrandId }, _mapper.Map<BrandDto>(brand));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] UpdateBrandDto updateBrandDto)
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);
            await _brandRepository.UpdateAsync(id, brand);
            _logger.LogInformation($"Updated brand with ID {id}");

            return NoContent();
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
