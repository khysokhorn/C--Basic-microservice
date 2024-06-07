using Entities;

namespace Repository
{
    public interface IItemRepository
    {
        public Task<IReadOnlyCollection<MovieItem>> GetAllAsync(int page, int pageSize);

        public Task<MovieItem> GetAsync(string movieID);

        public Task CreateAsync(MovieItem movie);

        public Task UpdateAsync(MovieItem movie);
    }
}