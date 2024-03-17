using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;

namespace Booking.ApiIntegration
{
    public class RoomApiClient : BaseApiClient, IRoomApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IRoomApiClient _RoomApiClient;

        public RoomApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedResult<RoomVm>> GetPagings(GetManageRoomPagingRequest request)
        {
            // Tạo URL với các tham số yêu cầu
            var url = $"/api/Rooms/Search?PageIndex={request.PageIndex}&PageSize={request.PageSize}";

            // Thêm tham số tìm kiếm theo tên sản phẩm nếu có
            if (!string.IsNullOrEmpty(request.RoomName))
            {
                url += $"&RoomName={request.RoomName}";
            }

            // Thêm tham số tìm kiếm theo loại sản phẩm nếu có
            if (!string.IsNullOrEmpty(request.RoomType))
            {
                url += $"&RoomType={request.RoomType}";
            }

            // Thêm tham số tìm kiếm theo mã chủ tiệc nếu có
            if (request.PartyHostId != null && request.PartyHostId != Guid.Empty)
            {
                url += $"&PartyHostId={request.PartyHostId}";
            }

            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<PagedResult<RoomVm>>(url);

            return data;
        }


        public async Task<bool> CreateRoom(RoomCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ProductUrl", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.PartyHostId.ToString() ?? ""), "PartyHostId");
            requestContent.Add(new StringContent(request.PartyId.ToString() ?? ""), "PartyId");
            requestContent.Add(new StringContent(request.RoomName ?? ""), "roomName");
            //requestContent.Add(new StringContent(request.ProductUrl ?? ""), "productUrl");
            requestContent.Add(new StringContent(request.RoomType?.ToString() ?? ""), "roomType");
            var requestContext = new MultipartFormDataContent();
            foreach (var productId in request.ProductIds)
            {
                requestContent.Add(new StringContent(productId.ToString()), "ProductIds");
            }
            requestContent.Add(new StringContent(request.Price?.ToString() ?? ""), "price");
            //requestContent.Add(new StringContent(languageId), "languageId");
            

            var response = await client.PostAsync($"/api/Rooms/Create", requestContent);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateRoom(RoomUpdateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.RoomId.ToString() ?? ""), "RoomId");
            requestContent.Add(new StringContent(request.RoomName ?? ""), "RoomName");
            //requestContent.Add(new StringContent(request.ProductUrl ?? ""), "productUrl");
            requestContent.Add(new StringContent(request.RoomType?.ToString() ?? ""), "RoomType");
            requestContent.Add(new StringContent(request.RoomStatus ?? ""), "RoomStatus");
            requestContent.Add(new StringContent(request.Price?.ToString() ?? ""), "Price");
            //requestContent.Add(new StringContent(request. ?? ""), "ProductStatus");
            //requestContent.Add(new StringContent(request.Description ?? ""), "Description");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/Rooms/Update", requestContent);
            return response.IsSuccessStatusCode;
        }


        //public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        //    var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await client.PutAsync($"/api/products/{id}/categories", httpContent);
        //    var result = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //        return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

        //    return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        //}

        public async Task<RoomVm> GetById(int id)
        {
            var data = await GetAsync<RoomVm>($"/api/Rooms/Get{id}");

            return data;
        }

        public async Task<List<RoomVm>> GetFeaturedProducts(int take)
        {
            var data = await GetListAsync<RoomVm>($"/api/Rooms/featured/{take}");
            return data;
        }

        public async Task<List<RoomVm>> GetLatestProducts( int take)
        {
            var data = await GetListAsync<RoomVm>($"/api/Rooms/latest/{take}");
            return data;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            return await Delete($"/api/Rooms/Delete/{id}");
        }

        public async Task<bool> ParentOrder (ParentOrder request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);



            var response = await client.PostAsJsonAsync($"/api/Rooms/Order", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddProducts(AddProductRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);



            var response = await client.PostAsJsonAsync($"/api/Rooms/AddProducts", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PartyHostComfirm(PHostComfirmRequest request)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            
            var url = $"/api/Rooms/PartyHostComfirm?PartyId={request.PartyId}&RoomId={request.RoomId}";

            

            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<int>(url);


            return data>0;
        }

        public Task<PagedResult<RoomVm>> GetPagingsParentRoom(GetManageRoomPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoomUpdateRequest>> GetFeaturedRooms(int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoomVm>> GetLatestRooms(int take)
        {
            throw new NotImplementedException();
        }
        //public async Task<List<RoomVm>> GetRoomsByPartyId(int partyId)
        //{
        //    // Assuming you have a PartyRoom table that maps parties to rooms
        //    var roomIds = await _context.PartyRoom
        //        .Where(pr => pr.PartyId == partyId)
        //        .Select(pr => pr.RoomId)
        //        .ToListAsync();

        //    // Retrieve rooms based on the retrieved room IDs
        //    var rooms = await _context.Rooms
        //        .Where(r => roomIds.Contains(r.RoomId))
        //        .ToListAsync();

        //    return rooms;
        //}

    }
}
