using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult Signup(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            return View();
        }
    }
}
