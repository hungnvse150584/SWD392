using Booking.ApiIntegration;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.System.Products;
using BookingSolution.ViewModels.System.Services;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Booking.AdminApp.Controllers
{
    public class ParentController : Controller
    {
        //private readonly IPartyHostApiClient partyHostApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly IPartyApiClient _partyApiClient;
        private readonly IConfiguration _configuration;
        private readonly IUserApiClient _userApiClient;
        private readonly IRoomApiClient _roomApiClient;
        private static string Bucket = "bpks-ee4a1.appspot.com";


        public ParentController(IProductApiClient productApiClient,
            IPartyApiClient partyApiClient,
            IConfiguration configuration,
            IUserApiClient userApiClient, IRoomApiClient roomApiClient)
        {
            _partyApiClient = partyApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
            _userApiClient = userApiClient;
            _roomApiClient = roomApiClient;
        }

        public IActionResult IndexHome()
        {
            var user = User.Identity.Name;
            return View();
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


        [HttpGet]
        public IActionResult ViewDetailParty(int partyid)
        {
           
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
            request.Status = "Active";

           
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

        

        public async Task<IActionResult> DetailsProduct(int id)
        {
            var result = await _productApiClient.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> DetailsRoom(DetailsRoom detailsRoom)
        {
            var party = await _partyApiClient.GetDetails(detailsRoom.PartyId);
            var result = party.roomUserViews.FirstOrDefault(x => x.RoomId == detailsRoom.RoomId);
            HttpContext.Session.SetString("PartyId", detailsRoom.PartyId.ToString());
            //var result = await _productApiClient.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> DetailsParty(int id)
        {
            var result = await _partyApiClient.GetDetails(id);
            return View(result);
        }

        public async Task<IActionResult> DetailsRoomBooked(RoomNParty roomNParty)
        {
            var userid = HttpContext.Session.GetString("UserId");
            var request = new DetailsRoomBookedRequest
            {
                Id = Guid.Parse(userid),
                roomId = roomNParty.roomId,
                partyId = roomNParty.PartyId,

            };
            var result = await _partyApiClient.DetailsRoomBooked(request);
            return View(result);
        }

        public async Task<IActionResult> History()
        {

            var userid = HttpContext.Session.GetString("UserId");
            var request = new PartyHistoryRequest
            {
                user = Guid.Parse(userid),

            };
            var result = await _partyApiClient.GetParentHistory(request);
            
            return View(result);
        }
        public async Task<IActionResult> Profile(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.Token);
        }
        //public ActionResult Order(AddProductRequest request)
        //{
        //    if (request != null)
        //    {
        //        request.PartyId = int.Parse(HttpContext.Session.GetString("PartyId"));
        //    }

        //    return View();
        //}




        //[HttpPost]
        public async Task<IActionResult> Order([FromBody]ParentOrder request)
        {
            if(request == null) return View(null);
            request.parentId = Guid.Parse(HttpContext.Session.GetString("UserId"));
            request.PartyId = int.Parse(HttpContext.Session.GetString("PartyId"));
            var result = await _roomApiClient.ParentOrder(request);
            if(result)
            {
                return Json(new { redirectToUrl = Url.Action("Success", "Parent") });
            }

            return Json(new { redirectToUrl = Url.Action("Fail", "Parent") });
        }
        public async Task<IActionResult> Success()
        {

           

            return View();
        }
        public async Task<IActionResult> Fail()
        {



            return View();
        }
    }
}
