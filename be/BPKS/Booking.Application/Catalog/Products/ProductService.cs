using Booking.Common;
using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
namespace Booking.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageService _storageService;
        private readonly BookingDbContext _context;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";


        public ProductService(BookingDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {

            
            var product = new Product()
            {

                ProductName = request.Productname,
                ProductUrl = await this.SaveFile(request.ThumbnailImage),
                ProductType = request.ProductType,
                ProductStyle = request.ProductStyle,
                Price = request.Price,

                //ThumbnailUrl = request.ThumbnailUrl,
                //DayStart = request.DayStart,
                //DayEnd = request.DayEnd,
                //CreatedDate = DateTime.Now,
                ProductStatus = request.Productstatus,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
           
        }
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new Exception($"Cannot find a product with id:{request.ProductId}.");
            }

            product.ProductName = request.ProductName;
            product.ProductUrl = await this.SaveFile(request.ThumbnailImage);
            product.ProductType = request.ProductType;
            product.ProductStyle = request.ProductStyle;
            product.Price = request.Price;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new BookingException($"Cannot find a product: {productId}");
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductVm>> GetAll()
        {
            var products = await _context.Products
                .Select(p => new ProductVm
                {
                    ProductId = p.ProductId,
                    PartyHostId = p.PartyHostId,
                    ProductName = p.ProductName,
                    ProductUrl = p.ProductUrl,
                    ProductType = p.ProductType,
                    ProductStyle = p.ProductStyle,
                    Price = p.Price,
                    ProductStatus = p.ProductStatus
                })
                .ToListAsync();

            return products;
        }

        public Task<List<ProductVm>> GetAllPaging(GetPublicProductPagingRequest request)
        {
            //var query = from p in _context.Products
            //            join pt in _context.
            throw new NotImplementedException();

        }

     

        public async Task<ProductVm> GetById(int productId)
        {
            var product = await _context.Products
                                  .Where(p => p.ProductId == productId)
                                  .Select(p => new ProductVm
                                  {
                                      ProductId = p.ProductId,
                                      PartyHostId = p.PartyHostId,
                                      ProductName = p.ProductName,
                                      ProductUrl = p.ProductUrl,
                                      ProductType = p.ProductType,
                                      ProductStyle = p.ProductStyle,
                                      Price = p.Price,
                                      ProductStatus = p.ProductStatus
                                  })
                                  .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new BookingException($"Cannot find a product with ID: {productId}");
            }

            return product;
        }

        //public async Task<List<ProductVm>> GetAll()
        //{
        //    // Truy vấn tất cả các sản phẩm từ cơ sở dữ liệu
        //    var products = await _context.Products.ToListAsync();

        //    // Chuyển đổi danh sách sản phẩm sang đối tượng ProductVm
        //    var productVms = products.Select(p => new ProductVm
        //    {
        //        ProductId = p.ProductId,
        //        PartyHostId = p.PartyHostId,
        //        ProductName = p.ProductName,
        //        ProductUrl = p.ProductUrl,
        //        ProductType = p.ProductType,
        //        Price = p.Price,
        //        ProductStatus = p.ProductStatus
        //        // Các thuộc tính khác của ProductVm
        //    }).ToList();

        //    return productVms;
        //}
        public Task<PagedResult<ProductVm>> GetAllByStyle(GetPublicProductPagingRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }

}
