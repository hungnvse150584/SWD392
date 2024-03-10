using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.ProductType;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.ProductTypes
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly BookingDbContext _context;

        public ProductTypeService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ProductTypeCreateRequest request)
        {
            var productType = new ProductType()
            {
                ProductTypeName = request.ProductTypeName,
                Status = request.Status,
                
            };
            _context.ProductTypes.Add(productType);
             await _context.SaveChangesAsync();
            return _context.ProductTypes.FirstOrDefault(p => p.ProductTypeName == productType.ProductTypeName).Id;
        }

        public async Task<int> Update(ProductTypeUpdateRequest request)
        {
            var productType = await _context.ProductTypes.FindAsync(request.Id);
            if (productType == null)
            {
                throw new Exception($"Cannot find a ProductType with id:{request.Id}.");
            }
            productType.Id = request.Id;
            productType.ProductTypeName =  request.ProductTypeName!=null?request.ProductTypeName:productType.ProductTypeName;
            productType.Status  = request.Status != null ? request.Status : productType.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var productType = await _context.ProductTypes.FindAsync(Id);
            if (productType == null)
            {
                throw new BookingException($"Cannot find a productType: {Id}");
            }

            _context.ProductTypes.Remove(productType);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<ProductTypeVm>> GetAll()
        {
            var productType = await _context.ProductTypes.ToListAsync();

            var productTypeVm = productType.Select(r => new ProductTypeVm
                {
                    Id = r.Id,
                    ProductTypeName = r.ProductTypeName,
                    Status = r.Status,
                })
                .ToList();

            return productTypeVm;
        }

        public async Task<ProductTypeVm> GetById(int Id)
        {
            var r = await _context.ProductTypes.FindAsync(Id);
            if (r == null)
            {
                return null;
            }

            var productType = new ProductTypeVm
            {
                Id = r.Id,
                ProductTypeName = r.ProductTypeName,
                Status = r.Status,
            };

            return productType;
        }
    }
}
