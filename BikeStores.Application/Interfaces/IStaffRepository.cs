using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStores.Domain.Models;

namespace BikeStores.Application.Interfaces
{
    public interface IStaffRepository : IAsyncRepository<Staff>
    {
        
    }
}