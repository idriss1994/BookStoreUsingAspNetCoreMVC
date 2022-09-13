using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; } = "Home from HomeController";
        public IActionResult Index()
        {
            CustomProperty = "Value from ViewData attribue";
            return View();
        }

        public IActionResult About(int age, string name)
        {
            return View();
        }
        //[Route("contact-us/{id}")]
        [HttpGet("~/contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
