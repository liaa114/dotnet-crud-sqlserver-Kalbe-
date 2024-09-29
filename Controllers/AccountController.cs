using Microsoft.AspNetCore.Mvc;
using Employe.Data;
using Employe.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Employe.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmployeContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(EmployeContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usernameOrEmail, string password)
        {
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username/Email and password are required.");
                return View();
            }

            var user = _context.MST_User
                .FirstOrDefault(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username!);
                HttpContext.Session.SetString("Name", user.Name ?? "");
                return RedirectToAction("AssetData", "Assets");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}