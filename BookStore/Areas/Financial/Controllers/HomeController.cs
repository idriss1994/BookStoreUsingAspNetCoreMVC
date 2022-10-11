using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Financial.Controllers
{
    [Area("financial")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            object s = "Index page from financial area";
            return View(s);
        }
    }
}
