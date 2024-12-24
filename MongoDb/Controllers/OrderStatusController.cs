using Microsoft.AspNetCore.Mvc;
using MongoDb.Dtos.OrderStatusDtos;
using MongoDb.Services.OrderStatusServices;

namespace MongoDb.Controllers
{
    public class OrderStatusController : Controller
    {
        private readonly IOrderStatusService _OrderStatusService;

        public OrderStatusController(IOrderStatusService OrderStatusService)
        {
            _OrderStatusService = OrderStatusService;
        }
        public async Task<IActionResult> OrderStatusList()
        {
            var value = await _OrderStatusService.GetAllOrderStatusAsync();
            return View(value);
        }
        [HttpGet]
        public IActionResult CreateOrderStatus()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderStatus(CreateOrderStatusDto createOrderStatusDto)
        {
            await _OrderStatusService.CreateOrderStatusAsync(createOrderStatusDto);
            return RedirectToAction("OrderStatusList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOrderStatus(string id)
        {
            var value = await _OrderStatusService.GetByIdOrderStatusAsync(id);
            UpdateOrderStatusDto updateOrderStatusDto = new UpdateOrderStatusDto()
            {
                OrderStatusId = id,
                OrderStatusName = value.OrderStatusName,
                
            };
            return View(updateOrderStatusDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatusDto)
        {
            await _OrderStatusService.UpdateOrderStatusAsync(updateOrderStatusDto);
            return RedirectToAction("OrderStatusList");
        }
        public async Task<IActionResult> DeleteOrderStatus(string id)
        {
            await _OrderStatusService.DeleteOrderStatusAsync(id);
            return RedirectToAction("OrderStatusList");

        }
    }
}
