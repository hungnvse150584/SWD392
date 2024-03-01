using Booking.AdminApp.Services;
using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration ) 
        {
        _userApiClient = userApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                //BearerToken = sessions,
                Keyword = keyword,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
            var data =await _userApiClient.GetUsersPaging(request);
            ViewBag.Keyword = keyword;
            return View(data);
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }
       

        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if(!ModelState.IsValid)
               return View(ModelState);
            var result = await _userApiClient.RegisterUser(request);
            if (result)
                return RedirectToAction("Index");
            return View(request);
        }
    }
}
