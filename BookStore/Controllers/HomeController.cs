using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using BookStore.Models;
using Microsoft.Extensions.Options;
using BookStore.Repository;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _optionsMonitor;
        private readonly IMessageRepository _messageRepository;

        public HomeController(IOptionsMonitor<NewBookAlertConfig> optionsMonitor, IMessageRepository messageRepository)
        {
            _optionsMonitor = optionsMonitor;
            _messageRepository = messageRepository;
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

            NewBookAlertConfig newBookAlertConfig = _optionsMonitor.CurrentValue;
            var value = _messageRepository.GetName();
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
