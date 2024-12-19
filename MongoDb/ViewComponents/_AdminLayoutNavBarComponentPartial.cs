using Microsoft.AspNetCore.Mvc;

namespace MongoDb.ViewComponents
{
    public class _AdminLayoutNavBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
