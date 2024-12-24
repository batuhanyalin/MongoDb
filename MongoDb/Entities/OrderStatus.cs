using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Entities
{
    public class OrderStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
    }
}
