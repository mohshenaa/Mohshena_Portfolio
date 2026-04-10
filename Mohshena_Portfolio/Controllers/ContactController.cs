using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
using Mohshena_Portfolio.ViewModels;

namespace Mohshena_Portfolio.Controllers
{
    public class ContactController(PortfolioDBContext db) : Controller
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeVM vm)
        {
            if (!ModelState.IsValid)
            {
                // reload data for view
                vm.Projects = db.Projects.ToList();
                vm.Skills = db.Skills.ToList();
                return View(vm);
            }

            db.ContactMessages.Add(vm.Contact);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }
    
    }
}
