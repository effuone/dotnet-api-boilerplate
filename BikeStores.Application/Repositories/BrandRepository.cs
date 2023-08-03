using BikeStores.Application.Interfaces;
using BikeStores.Domain.Context;
using BikeStores.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStores.Application.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(BikeStoresContext context) : base(context)
        {

        }
        public async Task<Brand> GetByNameAsync(string name)
        {
            return await _context.Brands.FirstOrDefaultAsync(brand => brand.BrandName == name);
        }
    }
}