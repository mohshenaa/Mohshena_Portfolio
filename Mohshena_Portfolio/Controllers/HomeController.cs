using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
using Mohshena_Portfolio.ViewModels;
using System.Diagnostics;

namespace Mohshena_Portfolio.Controllers
{
    public class HomeController(PortfolioDBContext db) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vm = new HomeVM
            {
                Projects = await db.Projects.ToListAsync(),
                Skills = await db.Skills.ToListAsync(),
                Contact = new ContactMessage()
            };

            return View(vm);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
