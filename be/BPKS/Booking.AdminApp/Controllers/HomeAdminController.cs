using Booking.ApiIntegration;
using Microsoft.AspNetCore.Mvc;

namespace Booking.AdminApp.Controllers
{
    //[Area("admin")]
    //[Route("admin")]
    //[Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        //[Route("")]
        //[Route("index")]

        private readonly ITotalApiClient _totalApiClient;
        public HomeAdminController(ITotalApiClient totalApiClient)
        {
            _totalApiClient = totalApiClient;
        }
        public async Task<IActionResult> Index()
        {
            double TotalCash = await _totalApiClient.GetTotalCash();
            int TotalPartyBook = await _totalApiClient.GetTotalPartyBooked();
            int TotalUser = await _totalApiClient.GetTotalUser();
            return ViewBag(TotalCash,TotalPartyBook,TotalUser);
        }
    }
}
