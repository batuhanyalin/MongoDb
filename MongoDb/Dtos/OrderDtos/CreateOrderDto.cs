namespace MongoDb.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatusId { get; set; }
    }
}
