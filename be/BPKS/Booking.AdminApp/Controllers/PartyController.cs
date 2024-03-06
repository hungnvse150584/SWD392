using Microsoft.AspNetCore.Mvc;

namespace Booking.AdminApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly IConfiguration _configuration;
        public IActionResult Index()
        {
            return View();
        }
    }
}
