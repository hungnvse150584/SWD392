using Booking.AdminApp.Models;
using Booking.ApiIntegration;
using BookingSolution.ViewModels.System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Booking.AdminApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITotalApiClient _totalApiClient;
        public HomeController(ILogger<HomeController> logger, ITotalApiClient totalApiClient)
        {
            _logger = logger;
            _totalApiClient = totalApiClient;
        }

        public async Task<IActionResult> Index()
        {
            double TotalCash = await _totalApiClient.GetTotalCash();
            int TotalPartyBook = await _totalApiClient.GetTotalPartyBooked();
            int TotalUser = await _totalApiClient.GetTotalUser();
            var cash = TotalCash.ToString("#,##0 VNĐ");
            var result = new Dashbroad{ TotalCash = cash, TotalPartyBooked = TotalPartyBook, TotalUser = TotalUser };
            return View(result);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
