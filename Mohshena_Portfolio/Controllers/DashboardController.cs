using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
using Mohshena_Portfolio.Services;
using Mohshena_Portfolio.ViewModels;

namespace Mohshena_Portfolio.Controllers
{
   
    [Authorize(Roles = "Admin")]
    public class DashboardController(PortfolioDBContext db) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var projects = await db.Projects.ToListAsync();
            var skills = await db.Skills.ToListAsync();
            var contacts = await db.ContactMessages.OrderByDescending(c => c.CreatedAt).ToListAsync();

            var vm = new DashboardVM
            {
                Projects = projects,
                Skills = skills,
                ContactMessages = contacts
            };

            return View(vm);
        }
       
    }
}
