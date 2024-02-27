using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.ViewComponents
{
    public class SideBarViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public SideBarViewComponent(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM header = new HeaderVM();

            header.User = new User();

            if (User.Identity.IsAuthenticated)
            {
                header.User = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            return View(header);
        }
    }
}
