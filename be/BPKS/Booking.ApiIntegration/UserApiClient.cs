using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace Booking.ApiIntegration
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/Users/authenticate", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
        }
    



    public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var queryParameters = new List<string>();

            // Kiểm tra và thêm tham số 'keyword' vào danh sách nếu có giá trị
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                queryParameters.Add($"keyword={request.Keyword}");
            }

            // Nối các tham số truy vấn để tạo URL hoàn chỉnh
            var queryString = string.Join("&", queryParameters);

            // Thêm tham số truy vấn vào URL nếu có
            var url = $"/api/Users/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}";
            if (!string.IsNullOrEmpty(queryString))
            {
                url += $"&{queryString}";
            }

            var response = await client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserVm>>>(body);
            return users;
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(request);
            //var httpContent = new StringContent(json, Encoding.UTF8, "multipart/form-data");
            var formData = new MultipartFormDataContent();

            // Thêm các trường dữ liệu từ RegisterRequest vào formData
            formData.Add(new StringContent(request.Email), "Email");
            formData.Add(new StringContent(request.Password), "Password");
            formData.Add(new StringContent(request.UserName), "UserName");
            formData.Add(new StringContent(request.FirstName), "FirstName");
            formData.Add(new StringContent(request.LastName), "LastName");
            formData.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            //formData.Add(new StringContent(request.Address), "Address");
            formData.Add(new StringContent(request.PhoneNumber), "PhoneNumber");
            formData.Add(new StringContent(request.Dob.ToString("yyyy-MM-dd")), "Dob");

            var response = await client.PostAsync($"/api/Users/Register", formData);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);

        }
        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Users/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<UserVm>> GetById(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Users/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserVm>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<UserVm>>(body);
        }
    }
}
