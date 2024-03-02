using Booking.ApiIntegration;
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

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
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
            return View(data.Token);
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
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
            if (result.IsSuccessed)
                return RedirectToAction("Index");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.Token;
                var updateRequest = new UserUpdateRequest()
                {
                    Id=user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Dob=user.Dob,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.Token);
        }
    }
}
