using Booking.ApiIntegration;
using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace  Booking.AdminApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomApiClient _roomApiClient;
        private readonly IConfiguration _configuration;

        //private readonly ICategoryApiClient _categoryApiClient;

        public RoomController(IRoomApiClient roomApiClient,
            IConfiguration configuration)
            //ICategoryApiClient categoryApiClient)
        {
            _configuration = configuration;
            _roomApiClient = roomApiClient;
            //_categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index(string searchField, string keyword, int pageIndex = 1, int pageSize = 10)
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

        public async Task<IActionResult> Details(int id)
        {
            var result = await _roomApiClient.GetById(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] RoomCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _roomApiClient.CreateRoom(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        //[HttpGet]
        //public async Task<IActionResult> CategoryAssign(int id)
        //{
        //    var roleAssignRequest = await GetCategoryAssignRequest(id);
        //    return View(roleAssignRequest);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return View();

        //    var result = await _productApiClient.CategoryAssign(request.Id, request);

        //    if (result.IsSuccessed)
        //    {
        //        TempData["result"] = "Cập nhật danh mục thành công";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", result.Message);
        //    var roleAssignRequest = await GetCategoryAssignRequest(request.Id);

        //    return View(roleAssignRequest);
        //}

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomApiClient.GetById(id);
            var editVm = new RoomUpdateRequest()
            {
                RoomId = room.RoomId,
                //PartyHostId = product.PartyHostId,
                RoomName = room.RoomName,
                //ProductUrl = product.ProductUrl,
                RoomStatus = room.RoomStatus,
                RoomType = room.RoomType,
                Price = room.Price,
                RoomUrl = room.RoomUrl,
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] RoomUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _roomApiClient.UpdateRoom(request);
            if (result)
            {
                TempData["result"] = "Cập nhật phòng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật phòng thất bại");
            return View(request);
        }

        //private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        //{
        //    var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

        //    var productObj = await _productApiClient.GetById(id);
        //    //var categories = await _categoryApiClient.GetAll(languageId);
        //    var categoryAssignRequest = new CategoryAssignRequest();
        //    //foreach (var role in categories)
        //    //{
        //    //    categoryAssignRequest.Categories.Add(new SelectItem()
        //    //    {
        //    //        Id = role.Id.ToString(),
        //    //        Name = role.Name,
        //    //        //Selected = productObj.Categories.Contains(role.Name)
        //    //    });
        //    //}
        //    //return categoryAssignRequest;
        //}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new RoomDeleteRequest()
            {
                RoomId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoomDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roomApiClient.DeleteRoom(request.RoomId);
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