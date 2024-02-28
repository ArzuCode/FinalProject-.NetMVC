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
    public class BioController : BaseController
    {


        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BioController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var bios = await _context.Bios.ToListAsync();

            return View(bios);
        }


        public async Task<IActionResult> Update()
        {
            Bio dbBio = await _context.Bios.FirstOrDefaultAsync();

            if (dbBio == null) return NotFound();

            return View(dbBio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Bio bio)
        {
            Bio dbBio = await _context.Bios.FirstOrDefaultAsync(b => b.Id == bio.Id);
            if (dbBio == null) return NotFound();

            if (ModelState["Image"] != null)
            {
                if (!dbBio.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Image Format Is Wrong");
                    return View();
                }
                if (bio.Image.ValidSize(8000))
                {
                    ModelState.AddModelError("Image", "Image Is Oversize");
                    return View();
                }


                string path = Path.Combine(_env.WebRootPath, @"assets\images", dbBio.ImageUrl);
                ImageService.DeleteImage(path);
                dbBio.ImageUrl = bio.Image.SaveImage(_env, @"assets\images");
            }

            dbBio.Location = bio.Location;
            dbBio.Contact = bio.Contact;
            dbBio.Email = bio.Email;
            dbBio.WorkTime = bio.WorkTime;

            await _context.SaveChangesAsync();
            return RedirectToAction("update","bio");
        }
    }
}
