using BikeStores.Application.Interfaces;
using BikeStores.Domain.Context;
using BikeStores.Domain.Models;

namespace BikeStores.Application.Repositories
{
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        public StaffRepository(BikeStoresContext context) : base(context)
        {

        }
    }
}