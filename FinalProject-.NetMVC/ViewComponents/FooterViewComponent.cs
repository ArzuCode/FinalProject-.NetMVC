using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio bio =await _context.Bios.FirstOrDefaultAsync();
            return View(await Task.FromResult(bio));
        }
    }
}
