using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Helpers;
using Rent_a_Car_.Net.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent_a_Car_.Net.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Controllers
{
    public class ElectricController : Controller
    {
        private readonly AppDbContext _context;

        public ElectricController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
        {
            ElectricVM electric = new ElectricVM();


            electric.Cars = await _context.Cars
                .Where(c => c.FuelType == Car.FuelTypes.Elektrik)
                .Include(c => c.Brand)
                .Include(c => c.CarImages)
                .Include(c => c.Color).ToListAsync();
            electric.PagedLists = PagedList<Car>.CreateAsync(electric.Cars, page, pageSize);

            return View(electric);
        }
    }
}
