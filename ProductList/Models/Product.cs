using MongoDB.Bson.Serialization.Attributes;

namespace ProductList.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
        [BsonElement("imageURL")]
        public string ImageURL { get; set; }
    }
}
