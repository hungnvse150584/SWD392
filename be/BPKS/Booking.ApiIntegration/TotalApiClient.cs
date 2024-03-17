using BookingSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApiIntegration
{
    public class TotalApiClient : BaseApiClient, ITotalApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public TotalApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<double> GetTotalCash()
        {
           var data = await GetAsync<double>($"/api/Total/GetTotalCash");
            return data;
        }

        public async Task<int> GetTotalPartyBooked()
        {
            var data = await GetAsync<int>($"/api/Total/GetTotalPartyBooked");
            return data;
        }

        public async Task<int> GetTotalUser()
        {
            var data = await GetAsync<int>($"/api/Total/GetTotalUser");
            return data;
        }

        
    }
}
