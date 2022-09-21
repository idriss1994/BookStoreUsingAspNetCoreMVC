using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
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
            var result = _configuration["AppName"];
            var section = _configuration.GetSection("InfoObj");
            var key11 = section.GetValue<string>("key1");
            var key1 = _configuration["InfoObj:Key1"];
            var key2 = _configuration["InfoObj:key2"];
            var key3 = _configuration["InfoObj:Key3:Key3Obj"];
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
