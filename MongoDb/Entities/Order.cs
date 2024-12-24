using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderStatusId {  get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
