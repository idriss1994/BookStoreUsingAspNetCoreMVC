using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using BookStore.Models;
using Microsoft.Extensions.Options;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IOptions<NewBookAlertConfig> _options;

        public HomeController(IOptions<NewBookAlertConfig> options)
        {
            _options = options;
        }

        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; } = "Home from HomeController";

        // ~ : to override the controller route lavel
        [Route("~/", Name = "Default route")]
        public IActionResult Index()
        {
            CustomProperty = "Value from ViewData attribue";

            NewBookAlertConfig newBookAlertConfig = _options.Value;

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
