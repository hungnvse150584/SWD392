using Booking.ApiIntegration;
using BookingSolution.ViewModels.Catalog.Categories;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.System.Languages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.AdminApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyApiClient _partyApiClient;
        private readonly IConfiguration _configuration;

        //private readonly ICategoryApiClient _categoryApiClient;

        public PartyController(IPartyApiClient partyApiClient,
            IConfiguration configuration)
        //ICategoryApiClient categoryApiClient)
        {
            _configuration = configuration;
            _partyApiClient = partyApiClient;
            //_categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index(string searchField, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetPublicPartyPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            // Xác định trường cần tìm kiếm dựa trên searchField
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchField)
                {
                    case "PartyName":
                        request.PartyName = keyword;
                        break;
                    case "Place":
                        request.Place = keyword;
                        break;
                        //default:
                        //    // Trả về trang với dữ liệu trống
                        //    return View(new PagedResult<ProductVm>());
                }
            }

            var data = await _partyApiClient.GetPagings(request);
            //ViewBag.ProductName = keyword;
            //ViewBag.ProductType = keyword;
            //ViewBag.PartyHostId= keyword;

            ViewBag.searchField = searchField;
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data);
        }
    }
}
