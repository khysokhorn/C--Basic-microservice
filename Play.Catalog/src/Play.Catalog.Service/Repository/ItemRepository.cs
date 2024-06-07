using Entities;
using MongoDB.Driver;

namespace Repository
{
    public class ItemRepository : IItemRepository
    {
        private string movieCollection = "movie";
        private readonly IMongoCollection<MovieItem> dbCollection;
        private readonly FilterDefinitionBuilder<MovieItem> filterBuilder = Builders<MovieItem>.Filter;

        public ItemRepository(IMongoDatabase database)
        {
            // var mongoClient = new MongoClient("mongodb+srv://root:2TWhEuU1nK6OagVA@moviester-cluster.92h0o9a.mongodb.net/");
            // var database = mongoClient.GetDatabase("Movie-Database");
            dbCollection = database.GetCollection<MovieItem>(movieCollection);
        }

        public async Task<IReadOnlyCollection<MovieItem>> GetAllAsync(int page, int pageSize)
        {
            return await dbCollection.Find(filterBuilder.Empty)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        }

        public async Task<MovieItem> GetAsync(string movieID)
        {
            FilterDefinition<MovieItem> filter = filterBuilder.Eq(e => e.Id, movieID);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(MovieItem movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            await dbCollection.InsertOneAsync(movie);
        }

        public async Task UpdateAsync(MovieItem movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            FilterDefinition<MovieItem> filter = filterBuilder.Eq(entity => entity.Id, movie.Id);
            await dbCollection.ReplaceOneAsync(filter, movie);
        }

    }
}