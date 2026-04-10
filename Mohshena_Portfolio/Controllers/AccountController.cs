using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mohshena_Portfolio.Data;

namespace Mohshena_Portfolio.Controllers
{
    public class AccountController(PortfolioDBContext db, SignInManager<IdentityUser> signInManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Invalid Login";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
