﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]//BsonType. veri türü formatıdır ve bunu belirler.
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
