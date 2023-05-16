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
    [Route("api/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IStoreRepository storeRepository, ILogger<StoreController> logger)
        {
            _storeRepository = storeRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _storeRepository.GetAllAsync();
            _logger.LogInformation($"Retrieved {stores.Count()} stores");
            return Ok(stores);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            var store = await _storeRepository.GetAsync(id);

            if (store == null)
            {
                _logger.LogInformation($"Store with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved store with ID {id}");
            return Ok(store);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] Store store)
        {
            var newStoreId = await _storeRepository.CreateAsync(store);

            _logger.LogInformation($"Created store with ID {newStoreId}");
            return CreatedAtAction(nameof(GetStore), new { id = newStoreId }, store);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] Store Store)
        {
            await _storeRepository.UpdateAsync(id, Store);

            _logger.LogInformation($"Updated store with ID {id}");
            return Ok(Store);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            await _storeRepository.DeleteAsync(id);

            _logger.LogInformation($"Deleted store with ID {id}");
            return NoContent();
        }
    }
}