using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.Controllers
{
    public class RentalController : BaseController
    {

        private readonly AppDbContext _context;

        public RentalController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Rental> rentals = await _context.Rentals
               .Include(r => r.User)
               .Include(r => r.Car)
               .ThenInclude(c => c.Brand)
               .Where(r => r.isDeleted == false)
               .ToListAsync();

            return View(rentals);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Rental dbRental = await _context.Rentals.FindAsync(id);

            if (dbRental == null) return NotFound();

            _context.Rentals.Remove(dbRental);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
