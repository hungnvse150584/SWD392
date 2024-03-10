using BookingSolution.Utilities.Constants;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace Booking.ApiIntegration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IProductApiClient _productApiClient;

        public ProductApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedResult<ProductVm>> GetPagings(GetManageProductPagingRequest request)
        {
            // Tạo URL với các tham số yêu cầu
            var url = $"/api/products/public-paging?PageIndex={request.PageIndex}&PageSize={request.PageSize}";

            // Thêm tham số tìm kiếm theo tên sản phẩm nếu có
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                url += $"&ProductName={request.ProductName}";
            }

            // Thêm tham số tìm kiếm theo loại sản phẩm nếu có
            if (request.ProductType != null && request.ProductType != 0)
            {
                url += $"&ProductType={request.ProductType}";
            }

            // Thêm tham số tìm kiếm theo mã chủ tiệc nếu có
            if (request.PartyHostId != null && request.PartyHostId != Guid.Empty)
            {
                url += $"&PartyHostId={request.PartyHostId}";
            }

            // Thực hiện yêu cầu HTTP GET đến URL đã tạo và nhận kết quả trả về
            var data = await GetAsync<PagedResult<ProductVm>>(url);

            return data;
        }


        public async Task<bool> CreateProduct(ProductCreateRequest request)
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
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.PartyHostId.ToString() ?? ""), "PartyHostId");
            requestContent.Add(new StringContent(request.Productname ?? ""), "productName");
            //requestContent.Add(new StringContent(request.ProductUrl ?? ""), "productUrl");
            requestContent.Add(new StringContent(request.ProductType?.ToString() ?? ""), "productType");
            requestContent.Add(new StringContent(request.ProductStyle ?? ""), "productStyle");
            requestContent.Add(new StringContent(request.Price?.ToString() ?? ""), "price");
            requestContent.Add(new StringContent(request.Productstatus ?? ""), "productStatus");
            requestContent.Add(new StringContent(request.Description ?? ""), "Description");
            //requestContent.Add(new StringContent(languageId), "languageId");
            

            var response = await client.PostAsync($"/api/products/Create", requestContent);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
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

            requestContent.Add(new StringContent(request.ProductId.ToString() ?? ""), "ProductId");
            requestContent.Add(new StringContent(request.ProductName ?? ""), "ProductName");
            //requestContent.Add(new StringContent(request.ProductUrl ?? ""), "productUrl");
            requestContent.Add(new StringContent(request.ProductType?.ToString() ?? ""), "ProductType");
            requestContent.Add(new StringContent(request.ProductStyle ?? ""), "ProductStyle");
            requestContent.Add(new StringContent(request.Price?.ToString() ?? ""), "Price");
            requestContent.Add(new StringContent(request.ProductStatus ?? ""), "ProductStatus");
            requestContent.Add(new StringContent(request.Description ?? ""), "Description");
            //requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/products/Update", requestContent);
            return response.IsSuccessStatusCode;
        }


        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/products/{id}/categories", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ProductVm> GetById(int id)
        {
            var data = await GetAsync<ProductVm>($"/api/products/{id}");

            return data;
        }

        public async Task<List<ProductVm>> GetFeaturedProducts(int take)
        {
            var data = await GetListAsync<ProductVm>($"/api/products/featured/{take}");
            return data;
        }

        public async Task<List<ProductVm>> GetLatestProducts( int take)
        {
            var data = await GetListAsync<ProductVm>($"/api/products/latest/{take}");
            return data;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await Delete($"/api/products/" + id);
        }

    }
}
