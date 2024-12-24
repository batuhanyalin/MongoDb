using AutoMapper;
using MongoDb.Dtos.OrderDtos;
using MongoDb.Entities;
using MongoDb.Settings;
using MongoDB.Driver;

namespace MongoDb.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IMongoCollection<OrderStatus> _orderStatusCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Customer> _customerCollection;

        public OrderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _mapper = mapper;
            _orderCollection = database.GetCollection<Order>(_databaseSettings.OrderCollectionName);
            _orderStatusCollection = database.GetCollection<OrderStatus>(_databaseSettings.OrderStatusCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var map = _mapper.Map<Order>(createOrderDto);
            await _orderCollection.InsertOneAsync(map);
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _orderCollection.DeleteOneAsync(x => x.OrderId == id);
        }

        public async Task<List<ResultOrderDto>> GetAllOrdersAsync()
        {
            var values = await _orderCollection.Find(x => true).ToListAsync();

            foreach (var value in values) 
            {
            var product= await _productCollection.Find(x=>x.ProductId== value.ProductId).FirstOrDefaultAsync();
            var customer= await _customerCollection.Find(x=>x.CustomerId== value.CustomerId).FirstOrDefaultAsync();
            var orderStat= await _orderStatusCollection.Find(x=>x.OrderStatusId== value.OrderStatusId).FirstOrDefaultAsync();
                

                value.Product = product;
                value.Customer = customer;
                value.OrderStatus = orderStat;
            }
            return _mapper.Map<List<ResultOrderDto>>(values);
        }

        public async Task<GetByIdOrderDto> GetByIdOrderAsync(string id)
        {
            var value = await _orderCollection.Find(x => x.OrderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOrderDto>(value);
        }

        public Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var value = _mapper.Map<Order>(updateOrderDto);
            return _orderCollection.FindOneAndReplaceAsync(x => x.OrderId == updateOrderDto.OrderId, value);
        }
    }
}
