using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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