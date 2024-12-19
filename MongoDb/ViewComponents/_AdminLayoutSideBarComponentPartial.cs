using Microsoft.AspNetCore.Mvc;

namespace MongoDb.ViewComponents
{
    public class _AdminLayoutSideBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
