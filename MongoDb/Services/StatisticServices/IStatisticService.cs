using MongoDb.Dtos.OrderDtos;
using MongoDb.Dtos.StatisticDtos;

namespace MongoDb.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<StatisticListDto> GetAllStatisticAsync();
    }
}
