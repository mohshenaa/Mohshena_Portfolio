using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
using Mohshena_Portfolio.Services;

namespace Mohshena_Portfolio.Controllers
{
    // [Authorize(Roles = "Admin")] // Uncomment when ready
    public class ProjectController : Controller
    {
        private readonly PortfolioDBContext _db;
        private readonly PhotoService _photoService;

        // Inject only PhotoService, remove IUploadService
        public ProjectController(PortfolioDBContext db, PhotoService photoService)
        {
            _db = db;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Projects.ToListAsync());
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Upload to Cloudinary and get the URL
                model.ImageUrl = await _photoService.AddPhotoAsync(ImageFile);
            }

            _db.Projects.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Dashboard");
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _db.Projects.FindAsync(id);
            if (data == null) return NotFound();
            return View(data);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project model, IFormFile? ImageFile)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            var existingProject = await _db.Projects.FindAsync(id);
            if (existingProject == null) return NotFound();

            // Update text fields
            existingProject.Title = model.Title;
            existingProject.Description = model.Description;
            existingProject.GitHubLink = model.GitHubLink;
            existingProject.LiveLink = model.LiveLink;

            // Handle new image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // (Optional) Delete old image from Cloudinary here if needed
                existingProject.ImageUrl = await _photoService.AddPhotoAsync(ImageFile);
            }
            // else keep existing ImageUrl

            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        // DELETE CONFIRMATION PAGE
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.Projects.FindAsync(id);
            if (data == null) return NotFound();
            return View(data);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _db.Projects.FindAsync(id);
            if (data != null)
            {
                _db.Projects.Remove(data);
                await _db.SaveChangesAsync();
                // (Optional) Delete image from Cloudinary here
            }
            return RedirectToAction("Index", "Dashboard");
        }

        // Optional: Separate endpoint for AJAX uploads (if you want)
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var imageUrl = await _photoService.AddPhotoAsync(file);
            return Ok(new { url = imageUrl });
        }
    }
}