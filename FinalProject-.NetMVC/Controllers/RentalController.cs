using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Rent_a_Car_.Net.Extentions;
using System.Collections.Generic;
using Rent_a_Car_.Net.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;
using Allup.Services;

namespace Rent_a_Car_.Net.Controllers
{
    public class RentalController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public RentalController(AppDbContext context, UserManager<User> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }


        public async Task<IActionResult> Index(int? id, string error)
        {
            if (id == null) return RedirectToAction("index", "car");

            RentDetailVM rentDetail = new RentDetailVM();

            if (User.Identity.IsAuthenticated)
            {
                rentDetail.User = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("login", "auth");
            }
            rentDetail.Car =await _context.Cars.Include(c => c.Brand).FirstOrDefaultAsync(c=>c.Id==id);

            if (error != null)
            {
                ModelState.AddModelError("Error", error);
                return View(rentDetail);
            }

            return View(rentDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(DateTime startDate, DateTime endDate, int carId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null) return View("Please login first");

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Color)
                .Include(c => c.CarImages)
                .FirstOrDefaultAsync(c => c.Id == carId);

            if (car == null) return NotFound();

            List<Rental> rental = await _context.Rentals.Where(r=>r.CarId==carId).ToListAsync();
            foreach (var item in rental)
            {
                if (item != null)
                {
                    var checkDate = startDate.InRange(item.RentDate, item.ReturnDate);
                    if (checkDate)
                    {
                        string error = $"Avtomobil {item.RentDate.ToString("MM/dd/yyyy")}'dən {item.ReturnDate.ToString("MM/dd/yyyy")}'dək icarədədir";
                        
                        return RedirectToAction("index", new {id=item.CarId, error=error});
                    }
                }

            }


            Rental newRental = new Rental();
            Random random = new Random();

            newRental.UserId = userId;
            newRental.CarId = carId;
            newRental.RentDate = startDate;
            newRental.ReturnDate = endDate;
            newRental.CreatedAt = DateTime.Now;
            
            

            await _context.AddAsync(newRental);
            await _context.SaveChangesAsync();

            newRental.InvoiceNo = random.Next(1, 99).ToString() + newRental.Id.ToString();

            await _context.SaveChangesAsync();

            return RedirectToAction("payment", new { id = newRental.Id});
        }

        public async Task<IActionResult> Invoice()
        {

            //var rental =await _context.Rentals
            //    .Include(r => r.User)
            //   .Include(r => r.Car)
            //   .ThenInclude(c => c.Brand)
            //   .FirstOrDefaultAsync(r => r.Id == id);

            ViewBag.Rental = await _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Car)
                .ThenInclude(c => c.Brand)
                .LastOrDefaultAsync();

            return View(); 
        }

        public IActionResult SendInvoice()
        {
            return View();
        }

        public byte[] InvoiceGenerator(int id)
        {
            var desktopView = new HtmlToPdf();
            desktopView.Options.WebPageWidth = 1024;


            var pdf = desktopView.ConvertUrl($"https://localhost:44300/rental/invoice/{id}");
            var pdfBytes = pdf.Save();

            return pdfBytes;
        }

        [Route("pay")]
        public async Task<dynamic> Pay(Payment payment)
        {
            if (!ModelState.IsValid) return RedirectToAction("payment");

            

            

            var result = await MakePayment.PayAsync(payment);

            if (result == "Success")
            {
                return RedirectToAction("invoice");
            }
            else
            {
                return RedirectToAction("Error", result);
            }
            
        }

        public IActionResult Payment(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("Error", "Belə avtomobil mövcud deyil");
                return RedirectToAction("rent");
            }

            var rental = _context.Rentals.Include(r => r.Car).FirstOrDefault(r => r.Id == id);
            var car = _context.Cars.Include(c => c.Brand).FirstOrDefault(c => c.Id == rental.CarId);
            ViewBag.Car = car;
            ViewBag.Rental = rental;

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
