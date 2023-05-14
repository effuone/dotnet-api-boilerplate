using BikeStores.Application.Interfaces;
using BikeStores.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeStores.Application.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly BikeStoresContext _context;

        public BaseRepository(BikeStoresContext context)
        {
            _context = context;
        }
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> CreateAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task UpdateAsync(int id, T model)
        {
            _context.Set<T>().Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await GetAsync(id);
            if (model != null)
            {
                _context.Set<T>().Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }
}