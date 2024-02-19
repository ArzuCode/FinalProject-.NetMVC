using Microsoft.AspNetCore.Mvc;

namespace Rent_a_Car_.Net.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return RedirectToAction("login","auth");
        }
    }
}
