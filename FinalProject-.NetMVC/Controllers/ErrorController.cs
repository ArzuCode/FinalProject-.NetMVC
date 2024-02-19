using Microsoft.AspNetCore.Mvc;

namespace Rent_a_Car_.Net.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
