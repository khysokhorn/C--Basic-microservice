using System.Linq.Expressions;
using Entities;

namespace Commom.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        public Task<IReadOnlyCollection<T>> GetAllAsync(int page, int pageSize);
        public Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter, int page, int pageSize);

        public Task<T> GetAsync(string Id);
        public Task RemoveAsync(string Id);
        public Task<T> GetAsync(Expression<Func<T, bool>> filter);

        public Task CreateAsync(T data);

        public Task UpdateAsync(T data);
    }
}