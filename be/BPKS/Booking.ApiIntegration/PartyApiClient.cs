using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking.ApiIntegration
{
    public class PartyApiClient : BaseApiClient, IPartyApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IProductApiClient _productApiClient;

        public PartyApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedResult<PartyVm>> GetPagings(GetPublicPartyPagingRequest request)
        {
            // Tạo URL với các tham số yêu cầu
            var url = $"/api/Parties/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}";

            // Thêm tham số tìm kiếm theo tên bữa tiệc nếu có
            if (!string.IsNullOrEmpty(request.PartyName))
            {
                url += $"&PartyName={request.PartyName}";
            }

            // Thêm tham số tìm kiếm theo nơi tổ chức nếu có
            if (!string.IsNullOrEmpty(request.Place))
            {
                url += $"&Place={request.Place}";
            }

            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<PagedResult<PartyVm>>(url);

            return data;
        
        
        }

    public async Task<bool> UpdatePartyDetail(PartyDetailsUpdateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);



            var response = await client.PutAsJsonAsync($"/api/Parties/UpdatePartyDetail", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStatus(UpdatePartyStatusRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);



            var response = await client.PutAsJsonAsync($"/api/Parties/UpdateStatus", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<PagedResult<PartyVm>> GetPagingsParentParty(GetPublicPartyPagingRequest request)
        {
            // Tạo URL với các tham số yêu cầu
            var url = $"/api/Parents/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}";

            // Thêm tham số tìm kiếm theo tên bữa tiệc nếu có
            if (!string.IsNullOrEmpty(request.PartyName))
            {
                url += $"&PartyName={request.PartyName}";
            }

            // Thêm tham số tìm kiếm theo nơi tổ chức nếu có
            if (!string.IsNullOrEmpty(request.Place))
            {
                url += $"&Place={request.Place}";
            }

            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<PagedResult<PartyVm>>(url);

            return data;
        }
        public async Task<List<PartyHistory>> GetPartyHostHistory(PartyHistoryRequest request)
        {
            // Tạo URL với các tham số yêu cầu
            var url = $"/api/Parties/GetPartyHostHistory?user={request.user}";
               

            // Thêm tham số tìm kiếm theo tên bữa tiệc nếu có
            if (!string.IsNullOrEmpty(request.status))
            {
                url += $"&status={request.status}";
            }

            // Thêm tham số tìm kiếm theo nơi tổ chức nếu có
           

            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<List<PartyHistory>>(url);

            return data;
        }

        public async Task<List<PartyHistory>> GetParentHistory(PartyHistoryRequest request)
        {
            // Tạo URL với các tham số yêu cầu
            var url = $"/api/Parties/GetParentHistory?user={request.user}";


            // Thêm tham số tìm kiếm theo tên bữa tiệc nếu có
            if (!string.IsNullOrEmpty(request.status))
            {
                url += $"&status={request.status}";
            }

            // Thêm tham số tìm kiếm theo nơi tổ chức nếu có


            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<List<PartyHistory>>(url);

            return data;
        }

        public async Task<PartyVm> GetById(int id)
        {
            var data = await GetAsync<PartyVm>($"/api/Parties/Get{id}");

            return data;
        }

        public async Task<bool> PartyComfirm(int id)
        {
            var data = await GetAsync<int>($"/api/Parties/PartyComfirm?request={id}");

            return data>0;
        }
        public async Task<bool> Checkout(int id)
        {
            var data = await GetAsync<int>($"/api/Parties/Checkout?request={id}");

            return data > 0;
        }

        public async Task<bool> FeedBack(FeedbackRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.PostAsJsonAsync($"/api/Parties/FeedBack", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateParty(PartyCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var userId = _httpContextAccessor
                .HttpContext
                .Session
                .GetString("UserId");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailUrl != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailUrl.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailUrl.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailUrl", request.ThumbnailUrl.FileName);
            }
            requestContent.Add(new StringContent(userId), "PartyHostId");
            requestContent.Add(new StringContent(request.PartyName ?? ""), "PartyName");
            requestContent.Add(new StringContent(request.Description ?? ""), "Description");
            requestContent.Add(new StringContent(request.PhoneContact ?? ""), "PhoneContact");
            requestContent.Add(new StringContent(request.Place ?? ""), "Place");
            requestContent.Add(new StringContent("0"), "Rate");
            requestContent.Add(new StringContent(DateTime.Now.ToString()), "CreatedDate");
            requestContent.Add(new StringContent(request.DayStart.ToString()), "DayStart");
            requestContent.Add(new StringContent(request.DayEnd.ToString()), "DayEnd");
            requestContent.Add(new StringContent("Active"), "PartyStatus");
            var requestContext = new MultipartFormDataContent();
            foreach (var productId in request.ProductId)
            {
                requestContent.Add(new StringContent(productId.ToString()), "ProductId");
            }
            foreach (var roomId in request.RoomId)
            {
                requestContent.Add(new StringContent(roomId.ToString()), "RoomId");
            }
            // Assuming SaveFile method returns the file URL asynchronously

            var response = await client.PostAsync($"/api/Parties/Create", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateParty(PartyUpdateRequest request)
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
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.PartyId.ToString()??""), "PartyId");
            requestContent.Add(new StringContent(request.PartyName ?? ""), "PartyName");
            requestContent.Add(new StringContent(request.PhoneContact ?? ""), "PhoneContact");
            requestContent.Add(new StringContent(request.Place ?? ""), "Place");
            //requestContent.Add(new StringContent("0"), "Rate");
            //requestContent.Add(new StringContent(DateTime.Now.ToString()), "CreatedDate");
            //requestContent.Add(new StringContent(request.DayStart.ToString()), "DayStart");
            requestContent.Add(new StringContent(request.DayEnd.ToString()), "DayEnd");
            //requestContent.Add(new StringContent("Active"), "PartyStatus");
            // Assuming SaveFile method returns the file URL asynchronously
            requestContent.Add(new StringContent(request.Description ?? ""), "Description");

            var response = await client.PutAsync($"/api/Parties/Update", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteParty(int id)
        {
            return await Delete($"/api/Parties/" + id);
        }

        public async Task<PartyUserView> GetDetails(int id)
        {
            var data = await GetAsync<PartyUserView>($"/api/Parties/GetPartyDetail?id={id}");

            return data;
        }
        public async Task<PartyUserView> DetailsRoomBooked(DetailsRoomBookedRequest request)
        {
            var data = await GetAsync<PartyUserView>($"/api/Parties/DetailsRoomBooked?id={request.Id}&partyId={request.partyId}&roomId={request.roomId}");

            return data;
        }
    }
}
