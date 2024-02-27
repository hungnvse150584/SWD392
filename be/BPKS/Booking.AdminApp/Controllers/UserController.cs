using Booking.AdminApp.Services;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace Booking.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        public UserController(IUserApiClient userApiClient ) 
        {
        _userApiClient = userApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
             if(!ModelState.IsValid)
                    return View(ModelState);

            var token = await _userApiClient.Authenticate(request);
            return View(token);
        }
    }
}
