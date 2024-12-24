using MongoDb.Dtos.OrderStatusDtos;

namespace MongoDb.Services.OrderStatusServices
{
    public interface IOrderStatusService
    {
        Task<List<ResultOrderStatusDto>> GetAllOrderStatusAsync();
        Task CreateOrderStatusAsync(CreateOrderStatusDto createOrderStatusDto);
        Task UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto);
        Task DeleteOrderStatusAsync(string id);
        Task<GetByIdOrderStatusDto> GetByIdOrderStatusAsync(string id);
    }
}
