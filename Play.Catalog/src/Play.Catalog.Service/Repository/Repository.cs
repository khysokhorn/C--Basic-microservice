using Entities;
using MongoDB.Driver;

namespace Repository
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private string movieCollection;
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string item)
        {
            dbCollection = database.GetCollection<T>(item);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(int page, int pageSize)
        {
            return await dbCollection.Find(filterBuilder.Empty)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        }

        public async Task<T> GetAsync(string movieID)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, movieID);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            await dbCollection.InsertOneAsync(movie);
        }

        public async Task UpdateAsync(T movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, movie.Id);
            await dbCollection.ReplaceOneAsync(filter, movie);
        }

    }
}