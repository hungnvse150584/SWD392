using Booking.ApiIntegration;
using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.System.Products;
using BookingSolution.ViewModels.System.Services;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Net.Http.Headers;

namespace Booking.AdminApp.Controllers
{
    public class PartyHostController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IRoomApiClient _roomApiClient;
        private readonly IPartyApiClient _partyApiClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserApiClient _userApiClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private static string Bucket = "bpks-ee4a1.appspot.com";


        public PartyHostController(IProductApiClient productApiClient,
            IPartyApiClient partyApiClient,
            IConfiguration configuration,
            IRoomApiClient roomApiClient,
            IHttpContextAccessor httpContextAccessor,
            IUserApiClient userApiClient,
            IHttpClientFactory httpClientFactory)
        {
            _partyApiClient = partyApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
            _roomApiClient = roomApiClient;
            _httpContextAccessor = httpContextAccessor;
            _userApiClient = userApiClient;
            _httpClientFactory = httpClientFactory;
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
            var sessions = _httpContextAccessor
            .HttpContext
            .Session
            .GetString(SystemConstants.AppSettings.Token);
                    var userId = _httpContextAccessor
            .HttpContext
            .Session
            .GetString("UserId");
            Guid guid = Guid.Parse(userId);

            var request = new GetPublicPartyPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                PartyHostId = guid
                
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

            var data = await _partyApiClient.GetPagingsPartyHostView(request);
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
        public async Task<IActionResult> CreateParty()
        {

            var request = new GetManageProductPagingRequest()
            {
                ProductName = null,
                PageIndex = 1,
                PageSize = 10
            };
            var roomrequest = new GetManageRoomPagingRequest()
            {
                RoomName = null,
                RoomType = null,
                PageIndex = 1,
                PageSize = 10
            };
            // Gọi service để lấy danh sách sản phẩm từ API
            var productsPagedResult = await _productApiClient.GetPagings(request); // Sử dụng phương thức GetAll hoặc phương thức tương tự trong service
            var products = productsPagedResult.Items;
            var roomsPagedResult = await _roomApiClient.GetPagings(roomrequest); // Sử dụng phương thức GetAll hoặc phương thức tương tự trong service
            var rooms = roomsPagedResult.Items;

            // Gán danh sách sản phẩm vào ViewBag
            ViewBag.Products = products;
            ViewBag.Rooms = rooms;
            // Gán danh sách sản phẩm vào ViewBag
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
                return RedirectToAction("IndexParty");
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
                return RedirectToAction("IndexProduct");
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

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productApiClient.GetById(id);
            var editVm = new ProductUpdateRequest()
            {
                ProductId = product.ProductId,
                //PartyHostId = product.PartyHostId,
                ProductName = product.ProductName,
                //ProductUrl = product.ProductUrl,
                ProductStyle = product.ProductStyle,
                ProductType = product.ProductType,
                Price = product.Price,
                ProductStatus = product.ProductStatus,
                Description = product.Description,
                ThumbnailImage = product.ThumbnailImage
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }
        [HttpGet]
        public IActionResult DeleteParty(int id)
        {
            return View(new PartyDeleteRequest()
            {
                PartyId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteParty(PartyDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _partyApiClient.DeleteParty(request.PartyId);
            if (result)
            {
                TempData["result"] = "Xóa tiệc thành công";
                return RedirectToAction("IndexParty");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            return View(new ProductDeleteRequest()
            {
                ProductId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.DeleteProduct(request.ProductId);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("IndexProduct");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        public IActionResult IndexHome()
        {
            var user = User.Identity.Name;
            return View();
        }

        public async Task<IActionResult> EditParty(int id)
        {
            var party = await _partyApiClient.GetById(id);
            var editVm = new PartyUpdateRequest()
            {
                PartyId = party.PartyId,
                PartyName = party.PartyName,
                PhoneContact = party.PhoneContact,
                Place = party.Place,
                DayStart = party.DayStart,
                DayEnd = party.DayEnd,
                Description = party.Description,
                ThumbnailImage = party.ThumbnailImage
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditParty([FromForm] PartyUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _partyApiClient.UpdateParty(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }
    }
}
