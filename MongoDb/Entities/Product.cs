using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.Metrics;
using System;

namespace MongoDb.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Bunun benzersiz olduğunu belirtmek için GUID değer almasını sağlıyoruz ve bu şekilde attribute giriyoruz[BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]//BsonType. veri türü formatıdır ve bunu belirler.
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CategoryId { get; set; }
    }
}
