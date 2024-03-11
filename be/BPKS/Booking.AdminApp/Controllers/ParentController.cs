using Booking.ApiIntegration;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.System.Products;
using BookingSolution.ViewModels.System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.AdminApp.Controllers
{
    public class ParentController : Controller
    {
        //private readonly IPartyHostApiClient partyHostApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly IPartyApiClient _partyApiClient;
        private readonly IConfiguration _configuration;
        private static string Bucket = "bpks-ee4a1.appspot.com";


        public ParentController(IProductApiClient productApiClient,
            IPartyApiClient partyApiClient,
            IConfiguration configuration)
        {
            _partyApiClient = partyApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> IndexProduct(string searchField, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            //string selectedFilter = filter;
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var request = new GetManageProductPagingRequest()
            {
                ProductName = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            // Xác định trường cần tìm kiếm dựa trên searchField
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchField)
                {
                    case "ProductName":
                        request.ProductName = keyword;
                        break;
                    case "ProductType":
                        request.ProductType = int.TryParse(keyword, out var productType) ? productType : (int?)null;
                        break;
                    case "PartyHostId":
                        request.PartyHostId = Guid.TryParse(keyword, out var partyHostId) ? partyHostId : (Guid?)null;
                        break;
                        //default:
                        //    // Trả về trang với dữ liệu trống
                        //    return View(new PagedResult<ProductVm>());
                }
            }

            var data = await _productApiClient.GetPagingsParentParty(request);
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

        public async Task<IActionResult> DetailsProduct(int id)
        {
            var result = await _productApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult ViewDetailParty(int partyid)
        {
            var view = new PartyUserView();
            var result =  _partyApiClient.GetDetails(partyid);
            return View(result);
        }

        public async Task<IActionResult> IndexParty(string searchField, string keyword, int pageIndex = 1, int pageSize = 10)
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


        public async Task<IActionResult> DetailsParty(int id)
        {
            var result = await _partyApiClient.GetDetails(id);
            return View(result);
        }
    }
}
