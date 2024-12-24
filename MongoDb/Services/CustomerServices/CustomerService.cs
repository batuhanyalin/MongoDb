using AutoMapper;
using MongoDb.Dtos.CustomerDtos;
using MongoDb.Entities;
using MongoDb.Settings;
using MongoDB.Driver;

namespace MongoDb.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;

        public CustomerService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var value = _mapper.Map<Customer>(createCustomerDto);
            await _customerCollection.InsertOneAsync(value);

        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(x => x.CustomerId == id);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomersAsync()
        {
            var values = await _customerCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCustomerDto>>(values);
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id)
        {
            var value = await _customerCollection.Find(x => x.CustomerId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCustomerDto>(value);
        }

        public Task UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var value = _mapper.Map<Customer>(updateCustomerDto);
            return _customerCollection.FindOneAndReplaceAsync(x => x.CustomerId == updateCustomerDto.CustomerId, value);
        }
    }
}
