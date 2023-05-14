using BikeStores.Domain.Models;

namespace BikeStores.Application.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        
    }
}