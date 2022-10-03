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
using Microsoft.AspNetCore.Identity;
using BookStore.Data;
using BookStore.Services;

namespace BookStore.Controllers
{
 //   [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _optionsMonitor;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly NewBookAlertConfig _newBookAlertConfig;
        private readonly NewBookAlertConfig _thirdPartyBookAlertConfig;

        public HomeController(IOptionsMonitor<NewBookAlertConfig> optionsMonitor,
            IMessageRepository messageRepository,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IUserService userService,
            IEmailService emailService)
        {
            _optionsMonitor = optionsMonitor;
            _messageRepository = messageRepository;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userService = userService;
            _emailService = emailService;
            _newBookAlertConfig = _optionsMonitor.Get("NewBookAlert");
            _thirdPartyBookAlertConfig = _optionsMonitor.Get("ThirdPartyBookAlert");
        }

        [ViewData]
        public string CustomProperty { get; set; }

        [ViewData]
        public string Title { get; set; } = "Home from HomeController";

        // ~ : to override the controller route lavel
        //[Route("/", Name = "Default route")]
        public async Task<IActionResult> Index()
        {
            var userEmailOptions = new UserEmailOptions()
            {
                ToEmails = new List<string> { "idriss@gmail.com" }
            };
            await _emailService.SendTestEmail(userEmailOptions);

            CustomProperty = "Value from ViewData attribue";

            string loggedInUserId = _userService.GetLoggedInUserId();
            bool isAuthenticated = _userService.IsAuthenticated();


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
