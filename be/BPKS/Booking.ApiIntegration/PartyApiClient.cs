using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<bool> CreateParty(PartyCreateRequest request)
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
            requestContent.Add(new StringContent(request.PartyHostId.ToString()), "PartyHostId");
            requestContent.Add(new StringContent(request.PartyName ?? ""), "PartyName");
            requestContent.Add(new StringContent(request.Description ?? ""), "Description");
            requestContent.Add(new StringContent(request.PhoneContact ?? ""), "PhoneContact");
            requestContent.Add(new StringContent(request.Place ?? ""), "Place");
            requestContent.Add(new StringContent("0"), "Rate");
            requestContent.Add(new StringContent(DateTime.Now.ToString()), "CreatedDate");
            requestContent.Add(new StringContent(request.DayStart.ToString()), "DayStart");
            requestContent.Add(new StringContent(request.DayEnd.ToString()), "DayEnd");
            requestContent.Add(new StringContent("Active"), "PartyStatus");
            // Assuming SaveFile method returns the file URL asynchronously

            var response = await client.PostAsync($"/api/Parties/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public Task<bool> DeleteParty(int id)
        {
            throw new NotImplementedException();
        }

        
        public Task<bool> UpdateParty(PartyUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
