using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM home = new HomeVM();

            home.Cars = await _context.Cars
                .Where(c => c.isDeleted == false)
                .Include(c => c.Brand)
                .Include(c => c.CarImages)
                .Include(c => c.Color).Take(6).ToListAsync();

            return View(home);
        }
    }
}