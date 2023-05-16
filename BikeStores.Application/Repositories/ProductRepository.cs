using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStores.Application.Interfaces;
using BikeStores.Domain.Context;
using BikeStores.Domain.Models;

namespace BikeStores.Application.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BikeStoresContext context) : base(context)
        {

        }
    }
}