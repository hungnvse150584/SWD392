using Booking.ApiIntegration;
using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Categories;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.System.Languages;
using BookingSolution.ViewModels.System.Products;
using BookingSolution.ViewModels.System.Services;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace Booking.AdminApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyApiClient _partyApiClient;
        private readonly IConfiguration _configuration;
        private static string Bucket = "bpks-ee4a1.appspot.com";
        //private readonly ICategoryApiClient _categoryApiClient;

        public PartyController(IPartyApiClient partyApiClient,
            IConfiguration configuration)
        //ICategoryApiClient categoryApiClient)
        {
            _configuration = configuration;
            _partyApiClient = partyApiClient;
            //_categoryApiClient = categoryApiClient;
        }

        [HttpGet]
        public IActionResult ViewDetail()
        {
            var view = new PartyUserView();
            return View(view);
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

        public async Task<IActionResult> Details(int id)
        {
            var result = await _partyApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] PartyCreateRequest request)
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

        public async Task<IActionResult> Edit(int id)
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
        public async Task<IActionResult> Edit([FromForm] PartyUpdateRequest request)
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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new PartyDeleteRequest()
            {
                PartyId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _partyApiClient.DeleteParty(request.ProductId);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }


    }
}
