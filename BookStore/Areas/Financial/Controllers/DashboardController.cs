using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Financial.Controllers
{
    [Area("financial")]
    public class DashboardController : Controller
    {
        public IActionResult Graph()
        {
            return View();
        }
    }
}
