using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeStores.Application.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly BikeStoresContext _context;

        public BaseRepository(BikeStoresContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public virtual async Task<bool> UpdateAsync(int id, T model)
        {
            _context.Set<T>().Update(model);
            var result = await _context.SaveChangesAsync();
            return result != 0;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var model = await GetAsync(id);
            if (model != null)
            {
                _context.Set<T>().Remove(model);
                var result = await _context.SaveChangesAsync();
                return result != 0;
            }
            return false;
        }
    }
}
