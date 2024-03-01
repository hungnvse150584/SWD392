using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Users;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Booking.AdminApp.Services
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
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7286");

            var response = await client.PostAsync("/api/Users/authenticate", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<PagedResult<UserVm>> GetUsersPaging(GetUserPagingRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            //var response = await client.GetAsync($"/api/Users/paging?&keyword={request.Keyword}&PageIndex={request.PageIndex}&PageSize={request.PageSize}");
            //        var response = await client.GetAsync($"/api/Users/paging?pageIndex=" +
            //$"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            // Tạo một danh sách các tham số truy vấn
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
            var users = JsonConvert.DeserializeObject<PagedResult<UserVm>>(body);
            return users;
        }

        public async Task<bool> RegisterUser(RegisterRequest request)
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
            formData.Add(new StringContent(request.ConfirmPassword), "ConfirmPassword");
            formData.Add(new StringContent(request.Address), "Address");
            formData.Add(new StringContent(request.PhoneNumber), "PhoneNumber");


            var response = await client.PostAsync($"/api/Users", formData);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
               
        }
        //    public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPagings(GetUserPagingRequest request)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

        //    client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        //    
        //    var body = await response.Content.ReadAsStringAsync();
        //    var users = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserVm>>>(body);
        //    return users;
        //}

    }
}
