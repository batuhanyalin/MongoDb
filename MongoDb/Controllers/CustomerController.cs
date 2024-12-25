using iTextSharp.text.pdf;
using iTextSharp.text;
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
                Surname = value.Surname,
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

        public async Task<IActionResult> CustomerDownload()
        {
            var customers = await _CustomerService.GetAllCustomersAsync();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/reports/pdfReports/" + "customerList.pdf");
            var stream = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);

            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font titleFont = new Font(baseFont, 18);

            Paragraph paragraph = new Paragraph("MongoDb - GoogleStorage Project - CustomerList", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(paragraph);

            document.Add(new Chunk("\n"));

            PdfPTable pdfPTable = new PdfPTable(3);
            pdfPTable.AddCell("MongoDbId");
            pdfPTable.AddCell("Customer Name");
            pdfPTable.AddCell("Customer Surname");

            foreach (var item in customers)
            {
                pdfPTable.AddCell(item.CustomerId);
                pdfPTable.AddCell(item.Name);
                pdfPTable.AddCell(item.Surname);
            }
            document.Add(pdfPTable);
            document.Close();
            return File("/reports/pdfReports/customerList.pdf", "application/pdf", "customerList.pdf");
        }
    }
}
