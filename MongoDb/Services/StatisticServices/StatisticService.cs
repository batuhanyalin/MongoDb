using AutoMapper;
using MongoDb.Dtos.StatisticDtos;
using MongoDB.Driver;
using MongoDb.Entities;
using MongoDb.Settings;

namespace MongoDb.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public StatisticService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _orderCollection = database.GetCollection<Order>(_databaseSettings.OrderCollectionName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _customerCollection = database.GetCollection<Customer>(_databaseSettings.CustomerCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }


        public async Task<StatisticListDto> GetAllStatisticAsync()
        {
            var categoryList = await _categoryCollection.Find(x => true).ToListAsync();
            int categoryCount = categoryList.Count;

            var productList = await _productCollection.Find(x => true).ToListAsync();
            int productCount = productList.Count;

            var orderList = await _orderCollection.Find(x => true).ToListAsync();
            int orderCount = orderList.Count;

            var customerList = await _customerCollection.Find(x => true).ToListAsync();
            int customerCount = customerList.Count;

            var productListForBeyazEsya = await _productCollection.Find(x => x.CategoryId == "676a96c8593b56d97b3e53f8").ToListAsync();
            int productListForBeyazEsyaCount = productListForBeyazEsya.Count;

            var productListForEkranKartı = await _productCollection.Find(x => x.CategoryId == "676a96de593b56d97b3e53fa").ToListAsync();
            int productListForEkranKartıCount = productListForEkranKartı.Count;

            var productListForKEA = await _productCollection.Find(x => x.CategoryId == "676a96d6593b56d97b3e53f9").ToListAsync();
            int productListForKEACount = productListForKEA.Count;

            var productListForTelefon = await _productCollection.Find(x => x.CategoryId == "676a930755549f82e36b2544").ToListAsync();
            int productListForTelefonCount = productListForTelefon.Count;

            StatisticListDto dto = new StatisticListDto()
            {
                categoryCount = categoryCount,
                productCount = productCount,
                customerCount = customerCount,
                orderCount = orderCount,
                productListForBeyazEsyaCount = productListForBeyazEsyaCount,
                productListForEkranKartıCount = productListForEkranKartıCount,
                productListForKEACount = productListForKEACount,
                productListForTelefonCount = productListForTelefonCount,
            };
            return dto;
        }
    }
}
