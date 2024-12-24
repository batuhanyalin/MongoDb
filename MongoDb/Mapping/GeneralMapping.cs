using AutoMapper;
using MongoDb.Dtos.CategoryDtos;
using MongoDb.Dtos.CustomerDtos;
using MongoDb.Dtos.OrderDtos;
using MongoDb.Dtos.OrderStatusDtos;
using MongoDb.Dtos.ProductDtos;
using MongoDb.Entities;

namespace MongoDb.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();//ReverseMap terse de mapleme yapabilmesini sağlar.
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetByIdProductDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, GetByIdOrderDto>().ReverseMap();

            CreateMap<OrderStatus, ResultOrderStatusDto>().ReverseMap();
            CreateMap<OrderStatus, CreateOrderStatusDto>().ReverseMap();
            CreateMap<OrderStatus, UpdateOrderStatusDto>().ReverseMap();
            CreateMap<OrderStatus, GetByIdOrderStatusDto>().ReverseMap();

            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetByIdCustomerDto>().ReverseMap();

        }
    }
}
