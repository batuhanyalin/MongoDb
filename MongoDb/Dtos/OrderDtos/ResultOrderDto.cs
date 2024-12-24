using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDb.Entities;
using MongoDb.Dtos.CategoryDtos;
using MongoDb.Dtos.ProductDtos;
using MongoDb.Dtos.CustomerDtos;
using MongoDb.Dtos.OrderStatusDtos;

namespace MongoDb.Dtos.OrderDtos
{
    public class ResultOrderDto
    {
        public string OrderId { get; set; }
        public ResultProductDto Product { get; set; }
        public ResultCustomerDto Customer { get; set; }
        public ResultOrderStatusDto OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
