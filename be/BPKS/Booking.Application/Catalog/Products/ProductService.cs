﻿using Booking.Common;
using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net.Sockets;
namespace Booking.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageService _storageService;
        private readonly BookingDbContext _context;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private static string Bucket = "bpks-ee4a1.appspot.com";

        public ProductService(BookingDbContext context, IStorageService storageService)
        {
            _storageService = storageService;
            _context = context;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                PartyHostId = request.PartyHostId,
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

        public async Task<PagedResult<ProductVm>> GetAllProducType(GetManageProductPagingRequest request)
        {
            //var query = from p in _context.Products
            //            join pt in _context.
            //select join
            var query =
            from p in _context.Products
            join pt in _context.ProductTypes on p.ProductType equals pt.Id
            where p.ProductType == request.ProductType
            select new { p, pt };
            //throw new NotImplementedException();
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.ProductName.Contains(request.Keyword));

            if (request.ProductType != null && request.ProductType != 0)
            {
                query = query.Where(p => p.pt.Id == request.ProductType);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductVm()
                {
                    ProductId = x.p.ProductId,
                    PartyHostId = x.p.PartyHostId,
                    ProductName = x.pt.ProductTypeName,
                    ProductUrl = x.p.ProductUrl,
                    ProductType = x.pt.Id,
                    ProductStyle = x.p.ProductStyle,
                    Price = x.p.Price,
                    ProductStatus = x.p.ProductStatus

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
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



        //private async Task<string> SaveFile(IFormFile file)
        //{
        //    var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
        //    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        //    var filePath = Path.Combine(USER_CONTENT_FOLDER_NAME, fileName);


        //    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }
        //    //await _storageService.SaveFileAsync(steam, fileName);


        //    return filePath;
        //}

        private async Task<string> SaveFile(IFormFile file)
        {

            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

            // Use OpenReadStream() directly to avoid potential stream closing issues
            //using (var stream = file.OpenReadStream())
            //{
            //    await _storageService.SaveFileAsync(stream, fileName);
            //}
            var task = new FirebaseStorage(Bucket,
               new FirebaseStorageOptions
               {
                   ThrowOnCancel = true
               })
               .Child("images")
               .Child("ProductImage")
               .Child(fileName)
               .PutAsync(file.OpenReadStream());
            return await task;
        }
    }

}
