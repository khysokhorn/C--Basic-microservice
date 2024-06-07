using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class MovieItem : IEntity
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string? Id { get; set; }

        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("cover")]
        public string Cover { get; set; }

        [BsonElement("imdb")]
        public string Imdb { get; set; }

        [BsonElement("year")]
        public string Year { get; set; }

        [BsonElement("quality")]
        public string Quality { get; set; }

        [BsonElement("duration")]
        public string Duration { get; set; }

        [BsonElement("page")]
        public int Page { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("count")]
        public int Count { get; set; }

        [BsonElement("created_all")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }


}
