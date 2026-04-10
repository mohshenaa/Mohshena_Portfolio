using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Mohshena_Portfolio.Controllers
{
 
    [Authorize(Roles = "Admin")]
    public class SkillController(PortfolioDBContext db) : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View(await db.Skills.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Skill model)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await db.Skills.FindAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Skill model)
        {
            db.Skills.Update(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await db.Skills.FindAsync(id);
            if (data == null) return NotFound();

            return View(data); // confirm page
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await db.Skills.FindAsync(id);

            db.Skills.Remove(data);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
