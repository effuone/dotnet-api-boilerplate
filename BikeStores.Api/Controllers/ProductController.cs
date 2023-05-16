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
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            _logger.LogInformation($"Retrieved {products.Count()} products");
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null)
            {
                _logger.LogInformation($"Product with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved product with ID {id}");
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var newProductId = await _productRepository.CreateAsync(product);

            _logger.LogInformation($"Created product with ID {newProductId}");
            return CreatedAtAction(nameof(GetProduct), new { id = newProductId }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            await _productRepository.UpdateAsync(id, product);

            _logger.LogInformation($"Updated product with ID {id}");
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteAsync(id);

            _logger.LogInformation($"Deleted product with ID {id}");
            return NoContent();
        }
    }
}