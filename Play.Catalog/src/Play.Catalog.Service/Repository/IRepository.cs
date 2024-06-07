using Entities;

namespace Repository
{
    public interface IRepository<T> where T : IEntity
    {
        public Task<IReadOnlyCollection<T>> GetAllAsync(int page, int pageSize);

        public Task<T> GetAsync(string Id);

        public Task CreateAsync(T data);

        public Task UpdateAsync(T data);
    }
}