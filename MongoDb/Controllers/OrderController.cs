using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDb.Dtos.OrderDtos;
using MongoDb.Services.CustomerServices;
using MongoDb.Services.OrderServices;
using MongoDb.Services.OrderStatusServices;
using MongoDb.Services.ProductServices;

namespace MongoDb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly IProductService _ProductService;
        private readonly ICustomerService _CustomerService;
        private readonly IOrderStatusService _OrderStatusService;

        public OrderController(IOrderService orderService, IProductService productService, ICustomerService customerService, IOrderStatusService orderStatusService)
        {
            _OrderService = orderService;
            _ProductService = productService;
            _CustomerService = customerService;
            _OrderStatusService = orderStatusService;
        }

        public async Task<IActionResult> OrderList()
        {
            var value = await _OrderService.GetAllOrdersAsync();
            return View(value);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var products = await _ProductService.GetAllProductsAsync();
            var customers = await _CustomerService.GetAllCustomersAsync();
            var orderStatus = await _OrderStatusService.GetAllOrderStatusAsync();
            List<SelectListItem> product = (from x in products.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ProductName,
                                            Value = x.ProductId
                                        }).ToList();
            ViewBag.productList = product;
            List<SelectListItem> customer = (from x in customers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = $"{x.Name} {x.Surname}",
                                                Value = x.CustomerId
                                            }).ToList();
            ViewBag.productList = product;
            List<SelectListItem> orderStat = (from x in orderStatus.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.OrderStatusName,
                                                Value = x.OrderStatusId
                                            }).ToList();
            ViewBag.productList = product;
            ViewBag.customerList = customer;
            ViewBag.orderStatusList = orderStat;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            await _OrderService.CreateOrderAsync(createOrderDto);
            return RedirectToAction("OrderList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(string id)
        {
            var products = await _ProductService.GetAllProductsAsync();
            var customers = await _CustomerService.GetAllCustomersAsync();
            var orderStatus = await _OrderStatusService.GetAllOrderStatusAsync();
            List<SelectListItem> product = (from x in products.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.ProductName,
                                                Value = x.ProductId
                                            }).ToList();
            ViewBag.productList = product;
            List<SelectListItem> customer = (from x in customers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = $"{x.Name} {x.Surname}",
                                                 Value = x.CustomerId
                                             }).ToList();
            ViewBag.productList = product;
            List<SelectListItem> orderStat = (from x in orderStatus.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.OrderStatusName,
                                                  Value = x.OrderStatusId
                                              }).ToList();
            ViewBag.productList = product;
            ViewBag.customerList = customer;
            ViewBag.orderStatusList = orderStat;


            var value = await _OrderService.GetByIdOrderAsync(id);
            UpdateOrderDto updateOrderDto = new UpdateOrderDto()
            {
                OrderId = id,
                CustomerId = value.CustomerId,
                OrderDate = DateTime.Now,
                OrderStatusId = value.OrderStatusId,
                ProductId=value.ProductId,
            };
            return View(updateOrderDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            await _OrderService.UpdateOrderAsync(updateOrderDto);
            return RedirectToAction("OrderList");
        }
        public async Task<IActionResult> DeleteOrder(string id)
        {
            await _OrderService.DeleteOrderAsync(id);
            return RedirectToAction("OrderList");

        }
    }
}
