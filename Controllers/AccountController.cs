using Microsoft.AspNetCore.Mvc;
using crmC_.service;
using crmC_.Models.request;
using Microsoft.AspNetCore.Http;

namespace crmC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var token = await _accountService.Login(authRequest);

            if (token != null)
            {
                Console.WriteLine("token isn't null");
                HttpContext.Session.SetString("AuthToken", token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
        }
    }
}
