using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDb.Dtos.ProductDtos;
using MongoDb.Services.CategoryServices;
using MongoDb.Services.ProductServices;

namespace MongoDb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductsAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var values = await _categoryService.GetAlllCategoryAsync();
            List<SelectListItem> cat = (from x in values.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.CategoryName,
                                            Value = x.CategoryId
                                        }).ToList();
            ViewBag.category = cat;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var value = await _productService.GetByIdProductAsync(id);
            UpdateProductDto updateProductDto = new UpdateProductDto()
            {
                ProductId = id,
                ProductName = value.ProductName,
                Stock = value.Stock,
                Price = value.Price,
                CategoryId=value.CategoryId,
            };
            return View(updateProductDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }
    }
}
