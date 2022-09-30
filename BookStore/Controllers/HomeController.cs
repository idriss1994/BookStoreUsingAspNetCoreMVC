﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using BookStore.Models;
using Microsoft.Extensions.Options;
using BookStore.Repository;
using Microsoft.AspNetCore.Identity;
using BookStore.Data;

namespace BookStore.Controllers
{
 //   [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _optionsMonitor;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        private readonly NewBookAlertConfig _newBookAlertConfig;
        private readonly NewBookAlertConfig _thirdPartyBookAlertConfig;

        public HomeController(IOptionsMonitor<NewBookAlertConfig> optionsMonitor,
            IMessageRepository messageRepository,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _optionsMonitor = optionsMonitor;
            _messageRepository = messageRepository;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _newBookAlertConfig = _optionsMonitor.Get("NewBookAlert");
            _thirdPartyBookAlertConfig = _optionsMonitor.Get("ThirdPartyBookAlert");
        }

        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; } = "Home from HomeController";

        // ~ : to override the controller route lavel
        //[Route("/", Name = "Default route")]
        public IActionResult Index()
        {
            CustomProperty = "Value from ViewData attribue";

            NewBookAlertConfig newBookAlertConfig = _newBookAlertConfig;
            NewBookAlertConfig thordPartyBookAlertConfig = _thirdPartyBookAlertConfig;
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
