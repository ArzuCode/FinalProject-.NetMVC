using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.Controllers
{
    public class BrandController : BaseController
    {


        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var brands =await _context.Brands.ToListAsync();

            return View(brands);
        }

		public IActionResult Create()
		{
			Brand brand = new Brand();
			return View(brand);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Brand brand)
		{
			if (!ModelState.IsValid) return View();

			Brand newbrand = new Brand
			{
				Name = brand.Name,
				CreatedAt = DateTime.Now,
				ImageUrl = brand.Image.SaveImage(_env, @"assets\images\brand"),
			};

			await _context.Brands.AddAsync(newbrand);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Brand dbBrand =await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);

            if (dbBrand == null) return NotFound();

            return View(dbBrand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Brand brand)
        {
            Brand dbBrand =await _context.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);
            if (dbBrand == null) return NotFound();

            if (ModelState["Image"] != null)
            {
                if (!brand.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Image Format Is Wrong");
                    return View();
                }
                if (brand.Image.ValidSize(8000))
                {
                    ModelState.AddModelError("Image", "Image Is Oversize");
                    return View();
                }


                string path = Path.Combine(_env.WebRootPath, @"assets\images\brand", dbBrand.ImageUrl);
                ImageService.DeleteImage(path);
                dbBrand.ImageUrl = brand.Image.SaveImage(_env, @"assets\images\brand");
            }


            var existName = _context.Brands.FirstOrDefault(x => x.Name.ToLower() == brand.Name.ToLower());

            if (existName != null)
            {
                if (dbBrand.Name.ToLower() != existName.Name.ToLower())
                {
                    ModelState.AddModelError("Name", "Bu brend artıq mövcuddur");
                    return View("Update");
                }
            }

            dbBrand.Name = brand.Name;
            dbBrand.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null) return NotFound();

            Brand dbBrand =await _context.Brands.FindAsync(id);

            if (dbBrand == null) return NotFound();

            dbBrand.isDeleted = true;
            dbBrand.DeletedAt = DateTime.Now;
            dbBrand.CreatedAt = null;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");

        }

        public async Task<IActionResult> RemovedBrands()
        {
            List<Brand> brands =await _context.Brands
                .Where(b => b.isDeleted == true).ToListAsync();

            return View(brands);
        }

        public async Task<IActionResult> BackUp(int id)
        {
            Brand deletedBrands =await _context.Brands.FindAsync(id);

            if (deletedBrands == null) return NotFound();
            deletedBrands.DeletedAt = null;
            deletedBrands.isDeleted = false;
            deletedBrands.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("removedbrands");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Brand dbBrand = await _context.Brands.FindAsync(id);

            if (dbBrand == null) return NotFound();

            if (dbBrand.ImageUrl == null) return NotFound();


            string path = Path.Combine(_env.WebRootPath, @"assets\images\brand", dbBrand.ImageUrl);
            ImageService.DeleteImage(path);

            _context.Brands.Remove(dbBrand);
            await _context.SaveChangesAsync();

            return RedirectToAction("removedbrands");
        }

    }
}
