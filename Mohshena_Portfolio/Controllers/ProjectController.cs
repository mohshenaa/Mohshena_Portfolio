using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
using Mohshena_Portfolio.Services;

namespace Mohshena_Portfolio.Controllers
{

   // [Authorize(Roles = "Admin")]
    public class ProjectController(PortfolioDBContext db, IUploadService uploadService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View(await db.Projects.ToListAsync());
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                model.ImageUrl = await uploadService.FileSave(model.ImageFile);
            }

            db.Projects.Add(model);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var data = await db.Projects.FindAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Project model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingProject = await db.Projects.FindAsync(model.Id);
            if (existingProject == null) return NotFound();

            // Update basic fields
            existingProject.Title = model.Title;
            existingProject.Description = model.Description;
            existingProject.GitHubLink = model.GitHubLink;
            existingProject.LiveLink = model.LiveLink;

            // Handle image upload
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                existingProject.ImageUrl = await uploadService.FileSave(model.ImageFile);
            }
            // else → DO NOTHING → keep old image ✅

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }

        // DELETE (SAFE VERSION)
        public async Task<IActionResult> Delete(int id)
        {
            var data = await db.Projects.FindAsync(id);
            if (data == null) return NotFound();

            return View(data); // confirm page
        }

        // DELETE CONFIRM
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await db.Projects.FindAsync(id);

            db.Projects.Remove(data);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
