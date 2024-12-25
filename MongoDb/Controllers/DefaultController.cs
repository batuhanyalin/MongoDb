using Microsoft.AspNetCore.Mvc;
using MongoDb.Services.StatisticServices;

namespace MongoDb.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IStatisticService _statisticService;

        public DefaultController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<IActionResult> Index()
        {
            var values= await _statisticService.GetAllStatisticAsync();
         
            return View(values);
        }
    }
}
