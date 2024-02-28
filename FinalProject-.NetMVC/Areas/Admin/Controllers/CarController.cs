using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.Controllers
{
    public class CarController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CarController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Car> cars =await _context.Cars
               .Include(c => c.CarImages)
               .Include(c => c.Color)
               .Include(c => c.Brand)
               .Where(c => c.isDeleted == false)
               .ToListAsync();
            return View(cars);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _context.Brands.Where(b => b.isDeleted == false).ToListAsync(), "Id", "Name");
            ViewBag.Colors = new SelectList(await _context.Colors.Where(c => c.isDeleted == false).ToListAsync(), "Id", "Name");
            Car car = new Car();
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Car car)
        {
            bool existCar = _context.Cars.Any(c => c.Name.ToLower() == car.Name.ToLower());
            if (existCar)
            {
                return StatusCode(409);
            }
            foreach (var item in car.Images)
            {
                if (!item.IsImage())
                {
                    ModelState.AddModelError("Error", "Fayl formatı düzgün deyil");
                    return View();
                }
                if (item.ValidSize(2000))
                {
                    ModelState.AddModelError("Error", "Fayl ölçüsü düzgün deyil");
                    return View();
                }
            }

            Car newCar = new Car();

            newCar.Name = car.Name;
            newCar.ModelYear = car.ModelYear;
            newCar.DailyPrice = car.DailyPrice;
            newCar.FuelType = car.FuelType;
            newCar.TransmissionType = car.TransmissionType;
            newCar.PassengerCount = car.PassengerCount;
            newCar.BrandId = car.BrandId;
            newCar.ColorId = car.ColorId;
            newCar.CreatedAt = DateTime.Now;

            await _context.AddAsync(newCar);
            await _context.SaveChangesAsync();

            foreach (var item in car.Images)
            {
                CarImage newcarImage = new CarImage();
                newcarImage.ImageUrl = item.SaveImage(_env, "assets/images/car");
                newcarImage.CarId = newCar.Id;
                if (item == car.Images[0])
                {
                    newcarImage.IsMain = true;
                }


                await _context.AddAsync(newcarImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Car dbCar =await _context.Cars
                .Include(c => c.CarImages)
                .Include(c => c.Brand)
                .Include(c => c.Color)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (dbCar == null) return NotFound();

            ViewBag.Brands = new SelectList(await _context.Brands.Where(b => b.isDeleted == false).ToListAsync(), "Id", "Name");
            ViewBag.Colors = new SelectList(await _context.Colors.Where(c => c.isDeleted == false).ToListAsync(), "Id", "Name");
            return View(dbCar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Car car)
        {
            Car dbCar = await _context.Cars
                .Include(c => c.CarImages)
                .Include(c => c.Brand)
                .Include(c => c.Color)
                .FirstOrDefaultAsync(c => c.Id == car.Id);

            if (dbCar == null)
            {
                ModelState.AddModelError("Error", "Avtomobil tapılmadı");
                return View("update");
            }

            if (car.Images != null)
            {
                foreach (var item in car.Images)
                {
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Images", "Fayl formatı düzgün deyil");
                        return View("Update");
                    }
                    if (item.ValidSize(2000))
                    {
                        ModelState.AddModelError("Images", "Fayl ölçüsü düzgün deyil");
                        return View("update");
                    }

                    CarImage newcarImage = new CarImage();
                    newcarImage.ImageUrl = item.SaveImage(_env, "assets/images/car");
                    newcarImage.CarId = car.Id;

                    await _context.AddAsync(newcarImage);
                    await _context.SaveChangesAsync();
                }
            }


            var existName =await _context.Cars.FirstOrDefaultAsync(x => x.Name.ToLower() == car.Name.ToLower());

            if (existName != null)
            {
                if (dbCar.Name.ToLower() != existName.Name.ToLower())
                {
                    ModelState.AddModelError("Name", "Avtomobil mövcuddur");
                    return View("Update");
                }
            }

            dbCar.Name = car.Name;
            dbCar.ModelYear = car.ModelYear;
            dbCar.DailyPrice = car.DailyPrice;
            dbCar.FuelType = car.FuelType;
            dbCar.TransmissionType = car.TransmissionType;
            dbCar.PassengerCount = car.PassengerCount;
            dbCar.BrandId = car.BrandId;
            dbCar.ColorId = car.ColorId;
            dbCar.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null) return NotFound();

            Car dbCar =await _context.Cars.FindAsync(id);

            if (dbCar == null) return NotFound();

            dbCar.isDeleted = true;
            dbCar.DeletedAt = DateTime.Now;
            dbCar.CreatedAt = null;


            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> RemovedCars()
        {

            List<Car> cars = await _context.Cars
               .Include(c => c.CarImages)
               .Include(c => c.Color)
               .Include(c => c.Brand)
               .Where(c => c.isDeleted == true)
               .ToListAsync();
            return View(cars);
        }

        public async Task<IActionResult> BackUp(int id)
        {
            Car deletedCars = await _context.Cars.FindAsync(id);

            if (deletedCars == null) return NotFound();

            deletedCars.DeletedAt = null;
            deletedCars.isDeleted = false;
            deletedCars.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("removedcars");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Car dbCar = await _context.Cars.FindAsync(id);

            if (dbCar == null) return NotFound();

            List<CarImage> dbCarImages = await _context.CarImages.Where(i => i.CarId == id).ToListAsync();

            if (dbCarImages == null) return NotFound();


            foreach (var item in dbCarImages)
            {
                string path = Path.Combine(_env.WebRootPath, @"assets\images\car", item.ImageUrl);
                ImageService.DeleteImage(path);
            }

            _context.Cars.Remove(dbCar);
            await _context.SaveChangesAsync();

            return RedirectToAction("removedcars");
        }

        public async Task<IActionResult> DeleteImage(int? id, int? carId, string returnUrl)
        {
            if (id == null || carId == null) return NotFound();
            var image = await _context.CarImages.FirstOrDefaultAsync(x => x.Id == id && x.CarId == carId);
            if (image == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, @"assets\images\car", image.ImageUrl);
            ImageService.DeleteImage(path);

            _context.CarImages.Remove(image);
            await _context.SaveChangesAsync();

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> ChangeMain(int? id, int? carId, string returnUrl)
        {
            if (id == null || carId == null) return NotFound();
            var image = await _context.CarImages.FirstOrDefaultAsync(x => x.Id == id && x.CarId == carId);
            if (image == null) return NotFound();

            var car = await _context.Cars.Include(x => x.CarImages).FirstOrDefaultAsync(x => x.Id == carId);
            var mainImage = car.CarImages.FirstOrDefault(x => x.IsMain);
            mainImage.IsMain = false;

            image.IsMain = true;
            await _context.SaveChangesAsync();

            return Redirect(returnUrl);
        }

    }
}
