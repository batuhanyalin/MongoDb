namespace MongoDb.Dtos.OrderDtos
{
    public class GetByIdOrderDto
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatusId { get; set; }
    }
}
