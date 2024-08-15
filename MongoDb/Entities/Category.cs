using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Bunun benzersiz olduğunu belirtmek için GUID değer almasını sağlıyoruz ve bu şekilde attribute giriyoruz [BsonRepresentation(BsonType.ObjectId)] 
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
