using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.Controllers
{
    public class ColorController : BaseController
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ColorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var colors = await _context.Colors.ToListAsync();

            return View(colors);
        }

		public IActionResult Create()
		{
			Color color = new Color();
			return View(color);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Color color)
		{
			if (!ModelState.IsValid) return View();

			Color newColor = new Color
			{
				Name = color.Name,
				CreatedAt = DateTime.Now
			};

			await _context.Colors.AddAsync(newColor);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Color dbColor = await _context.Colors.FirstOrDefaultAsync(b => b.Id == id);

            if (dbColor == null) return NotFound();

            return View(dbColor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Color color)
        {
            Color dbColor = await _context.Colors.FirstOrDefaultAsync(c => c.Id == color.Id);
            if (dbColor == null) return NotFound();


            var existName =await _context.Colors.FirstOrDefaultAsync(c => c.Name.ToLower() == color.Name.ToLower());

            if (existName != null)
            {
                if (dbColor.Name.ToLower() != existName.Name.ToLower())
                {
                    ModelState.AddModelError("Name", "Bu rəng artıq mövcuddur");
                    return View("Update");
                }
            }

            dbColor.Name = color.Name;
            dbColor.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null) return NotFound();

            Color dbColor = await _context.Colors.FindAsync(id);

            if (dbColor == null) return NotFound();

            dbColor.isDeleted = true;
            dbColor.DeletedAt = DateTime.Now;
            dbColor.CreatedAt = null;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");

        }

        public async Task<IActionResult> RemovedColors()
        {
            List<Color> colors = await _context.Colors
                .Where(c => c.isDeleted == true).ToListAsync();

            return View(colors);
        }

        public async Task<IActionResult> BackUp(int id)
        {
            Color deletedColors = await _context.Colors.FindAsync(id);

            if (deletedColors == null) return NotFound();
            deletedColors.DeletedAt = null;
            deletedColors.isDeleted = false;
            deletedColors.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("removedcolors");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Color dbColor = await _context.Colors.FindAsync(id);

            if (dbColor == null) return NotFound();

            _context.Colors.Remove(dbColor);
            await _context.SaveChangesAsync();

            return RedirectToAction("removedcolors");
        }
    }
}
