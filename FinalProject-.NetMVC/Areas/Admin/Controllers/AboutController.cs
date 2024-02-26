using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _context.About.ToListAsync();

            return View(about);
        }


        public async Task<IActionResult> Update()
        {
            About dbAbout = await _context.About.FirstOrDefaultAsync();

            if (dbAbout == null) return NotFound();

            return View(dbAbout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(About about)
        {
            About dbAbout = await _context.About.FirstOrDefaultAsync(a => a.Id == about.Id);
            if (dbAbout == null) return NotFound();

            if (ModelState["Image"] != null)
            {
                if (!dbAbout.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Image Format Is Wrong");
                    return View();
                }
                if (about.Image.ValidSize(8000))
                {
                    ModelState.AddModelError("Image", "Image Is Oversize");
                    return View();
                }


                string path = Path.Combine(_env.WebRootPath, @"assets\images", dbAbout.ImageUrl);
                ImageService.DeleteImage(path);
                dbAbout.ImageUrl = about.Image.SaveImage(_env, @"assets\images");
            }

            dbAbout.Title = about.Title;
            dbAbout.HeaderDesc = about.HeaderDesc;
            dbAbout.Desc = about.Desc;

            await _context.SaveChangesAsync();
            return RedirectToAction("update","about");
        }
    }
}
