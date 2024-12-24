using AutoMapper;
using MongoDb.Dtos.OrderStatusDtos;
using MongoDb.Dtos.OrderStatusDtos;
using MongoDb.Entities;
using MongoDb.Settings;
using MongoDB.Driver;

namespace MongoDb.Services.OrderStatusServices
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IMongoCollection<OrderStatus> _orderStatusCollection;
        private readonly IMapper _mapper;

        public OrderStatusService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
           var client= new MongoClient(_databaseSettings.ConnectionString);
            var database= client.GetDatabase(_databaseSettings.DatabaseName);
            _orderStatusCollection = database.GetCollection<OrderStatus>(_databaseSettings.OrderStatusCollectionName);
            _mapper = mapper;
        }

        public async Task CreateOrderStatusAsync(CreateOrderStatusDto createOrderStatusDto)
        {
            var value = _mapper.Map<OrderStatus>(createOrderStatusDto);
            await _orderStatusCollection.InsertOneAsync(value);
        }

        public async Task DeleteOrderStatusAsync(string id)
        {
            await _orderStatusCollection.DeleteOneAsync(x => x.OrderStatusId == id);
        }

        public async Task<List<ResultOrderStatusDto>> GetAllOrderStatusAsync()
        {
            var values = await _orderStatusCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultOrderStatusDto>>(values);
        }

        public async Task<GetByIdOrderStatusDto> GetByIdOrderStatusAsync(string id)
        {
            var value = await _orderStatusCollection.Find(x => x.OrderStatusId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOrderStatusDto>(value);
        }

        public async Task UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto)
        {
            var value = _mapper.Map<OrderStatus>(updateOrderStatusDto);
            await _orderStatusCollection.FindOneAndReplaceAsync(x => x.OrderStatusId == updateOrderStatusDto.OrderStatusId, value);
        }
    }
}
