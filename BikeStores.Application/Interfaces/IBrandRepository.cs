using BikeStores.Domain.Models;

namespace BikeStores.Application.Interfaces
{
    public interface IBrandRepository : IAsyncRepository<Brand>
    {
        Task<Brand> GetByNameAsync(string name);
    }
}