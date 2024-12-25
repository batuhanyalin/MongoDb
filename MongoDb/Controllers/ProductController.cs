using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDb.Dtos.ProductDtos;
using MongoDb.Services.CategoryServices;
using MongoDb.Services.ProductServices;
using OfficeOpenXml;

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
            ViewBag.categoryList = cat;
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
                CategoryId = value.CategoryId,
                CurrentImage = value.ImageUrl,
            };
            var values = await _categoryService.GetAlllCategoryAsync();
            List<SelectListItem> cat = (from x in values.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.CategoryName,
                                            Value = x.CategoryId
                                        }).ToList();
            ViewBag.categoryList = cat;
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

        public async Task<IActionResult> ProductDownload()
        {
            var products = await _productService.GetAllProductsAsync();
            //ExcelPackage excel = new ExcelPackage();
            //var workSheet = excel.Workbook.Worksheets.Add("Sayfa1");
            //workSheet.Cells[1, 1].Value = "Ürün Adı";
            //workSheet.Cells[1, 2].Value = "Kategori";
            //workSheet.Cells[1, 3].Value = "Stok";
            //workSheet.Cells[1, 4].Value = "Fiyat";
            //workSheet.Cells[1, 5].Value = "Görsel";


            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Ürün Listesi");
                workSheet.Cell(1, 1).Value = "Ürün Adı";
                workSheet.Cell(1, 2).Value = "Kategori";
                workSheet.Cell(1, 3).Value = "Stok";
                workSheet.Cell(1, 4).Value = "Fiyat";

                int rowCount = 2;

                foreach (var item in products)
                {
                    workSheet.Cell(rowCount, 1).Value = item.ProductName;
                    workSheet.Cell(rowCount, 2).Value = item.Category.CategoryName;
                    workSheet.Cell(rowCount, 3).Value = item.Stock;
                    workSheet.Cell(rowCount, 4).Value = item.Price;
                    rowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content=stream.ToArray();
                    return File(content,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","productList.xlsx");
                }
            }
        }

    }
}
