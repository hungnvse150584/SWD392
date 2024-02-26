﻿using Booking.Application.Catalog.Parties.Dtos;
using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly BookingDBContext _context;
        public ManageProductService(BookingDBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                ProductName = request.Productname,
                ProductUrl = request.ProductUrl,
                ProductType = request.ProductType,
                ProductStyle = request.ProductStyle,
                Price = request.Price,
                
                //ThumbnailUrl = request.ThumbnailUrl,
                //DayStart = request.DayStart,
                //DayEnd = request.DayEnd,
                //CreatedDate = DateTime.Now,
                //PartyStatus = request.PartyStatus,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null) throw new Exception($"Cannot find a product with id:{request.ProductId}.");

            product.ProductName = request.ProductName;
            product.ProductUrl = request.ProductUrl;
            product.ProductType = request.ProductType;
            product.ProductStyle = request.ProductStyle;
            product.Price = request.Price;
                //ProductUrl = request.ProductUrl,
                //ProductType = request.ProductType,
                //ProductStyle = request.ProductStyle,
                //Price = request.Price,
                //ThumbnailUrl = request.ThumbnailUrl,
                //DayStart = request.DayStart,
                //DayEnd = request.DayEnd,
                //CreatedDate = DateTime.Now,
                //PartyStatus = request.PartyStatus,
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null) throw new BookingException($"Cannot find a product: {productId}");
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public Task<List<ProductVm>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVm>> GetAllPaging(GetPublicProductPagingRequest request)
        {
            //var query = from p in _context.Products
            //            join pt in _context.
            throw new NotImplementedException();

        }
    }
}