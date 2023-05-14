namespace BikeStores.Application.Interfaces
{
    //common interface for basic CRUD in repositories
    public interface IAsyncRepository<T> where T: class
    {
        public Task<T> GetAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> CreateAsync(T model);
        public Task UpdateAsync(int id, T model);
        public Task DeleteAsync(int id);
    }
}