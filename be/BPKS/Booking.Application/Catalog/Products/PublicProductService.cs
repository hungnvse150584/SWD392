using Booking.Data.EF;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly BookingDBContext _context;
        public PublicProductService(BookingDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVm>> GetAll()
        {
            // Truy vấn tất cả các sản phẩm từ cơ sở dữ liệu
            var products = await _context.Products.ToListAsync();

            // Chuyển đổi danh sách sản phẩm sang đối tượng ProductVm
            var productVms = products.Select(p => new ProductVm
            {
                ProductId = p.ProductId,
                PartyHostId = p.PartyHostId,
                ProductName = p.ProductName,
                ProductUrl = p.ProductUrl,
                ProductType = p.ProductType,
                Price = p.Price,
                ProductStatus = p.ProductStatus
                // Các thuộc tính khác của ProductVm
            }).ToList();

            return productVms;
        }

        //test
        public Task<PagedResult<ProductVm>> GetAllByStyle(GetPublicProductPagingRequest request)
        {
            throw new NotImplementedException();
        }

        //public async Task<PagedResult<ProductVm>> GetAllByStyle(GetPublicProductPagingRequest request)
        //{
        //    //1.Select join 
        //    var query = from p in _context.Products
        //                join lp in _context.ListProducts on p.ProductId equals lp.ProductId
        //                select new { p, lp };
        //    //2.filter
        //    if( request.)

        //}
    }
}
