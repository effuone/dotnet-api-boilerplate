using BikeStores.Application.Repositories;
using BikeStores.Domain.Context;
using BikeStores.Domain.Models;

namespace BikeStores.Application.Interfaces
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BikeStoresContext context) : base(context)
        {

        }
    }
}