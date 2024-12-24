using Microsoft.AspNetCore.Mvc;
using MongoDb.Dtos.CustomerDtos;
using MongoDb.Services.CustomerServices;

namespace MongoDb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _CustomerService;

        public CustomerController(ICustomerService CustomerService)
        {
            _CustomerService = CustomerService;
        }
        public async Task<IActionResult> CustomerList()
        {
            var value = await _CustomerService.GetAllCustomersAsync();
            return View(value);
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            await _CustomerService.CreateCustomerAsync(createCustomerDto);
            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(string id)
        {
            var value = await _CustomerService.GetByIdCustomerAsync(id);
            UpdateCustomerDto updateCustomerDto = new UpdateCustomerDto()
            {
                CustomerId = id,
                Name = value.Name,
                Surname=value.Surname,
            };
            return View(updateCustomerDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            await _CustomerService.UpdateCustomerAsync(updateCustomerDto);
            return RedirectToAction("CustomerList");
        }
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _CustomerService.DeleteCustomerAsync(id);
            return RedirectToAction("CustomerList");

        }
    }
}
