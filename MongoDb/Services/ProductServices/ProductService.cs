using AutoMapper;
using MongoDb.Dtos.ProductDtos;
using MongoDb.Entities;
using MongoDb.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;


        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var products = await _productCollection.Find(x => true).ToListAsync();

            // İlişkileri manuel kuruyoruz
            foreach (var product in products)
            {
                var category = await _categoryCollection
                    .Find(x => x.CategoryId == product.CategoryId)
                    .FirstOrDefaultAsync();

                product.Category = category; // İlgili kategoriyi atıyoruz
            }
            return _mapper.Map<List<ResultProductDto>>(products);
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(value); //MongoDbde Insert işlemi InsertOneAsync metoduyla sağlanır.
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }

        public Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            return _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, value);
        }
    }
}