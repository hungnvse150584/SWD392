using Booking.ApiIntegration;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.System.Products;
using BookingSolution.ViewModels.System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.AdminApp.Controllers
{
    public class PartyHostController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IRoomApiClient _roomApiClient;
        private readonly IPartyApiClient _partyApiClient;
        private readonly IConfiguration _configuration;
        private static string Bucket = "bpks-ee4a1.appspot.com";


        public PartyHostController(IProductApiClient productApiClient,
            IPartyApiClient partyApiClient,
            IConfiguration configuration,
            IRoomApiClient roomApiClient)
        {
            _partyApiClient = partyApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
            _roomApiClient = roomApiClient;
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
        public IActionResult ViewDetailParty()
        {
            var view = new PartyUserView();
            return View(view);
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
            var result = await _partyApiClient.GetById(id);
            return View(result);
        }


        [HttpGet]
        public IActionResult CreateParty()
        {
            return View();
        }

        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateParty([FromForm] PartyCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _partyApiClient.CreateParty(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProuct([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
        public async Task<IActionResult> IndexRoom(string searchField, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            //string selectedFilter = filter;
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var request = new GetManageRoomPagingRequest()
            {
                RoomName = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            // Xác định trường cần tìm kiếm dựa trên searchField
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchField)
                {
                    case "RoomName":
                        request.RoomName = keyword;
                        break;
                    case "RoomType":
                        request.RoomType = keyword;
                        break;
                    case "PartyHostId":
                        request.PartyHostId = Guid.TryParse(keyword, out var partyHostId) ? partyHostId : (Guid?)null;
                        break;
                        //default:
                        //    // Trả về trang với dữ liệu trống
                        //    return View(new PagedResult<ProductVm>());
                }
            }

            var data = await _roomApiClient.GetPagings(request);
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
