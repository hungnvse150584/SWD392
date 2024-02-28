using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Parents;
using BookingSolution.ViewModels.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parents
{
    public class ManageParentService : IManageParentService
    {
        private readonly BookingDBContext _context;
        public ManageParentService(BookingDBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ParentCreateRequest request)
        {
            var parent = new Parent()
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Email = request.Email,
               
            };
            _context.Parents.Add(parent);
            return await _context.SaveChangesAsync();


        }

        public async Task<int> Delete(int parentId)
        {
            var parent = await _context.Products.FindAsync(parentId);
            if (parent == null)
            {
                throw new BookingException($"Cannot find parent: {parentId}");
            }

            _context.Products.Remove(parent);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ParentVm>> GetAll()
        {
            var parent = await _context.Parents
               .Select(p => new ParentVm
               {
                   ParentId = p.ParentId,
                   UserName = p.UserName,
                   Password = p.Password,
                   FullName = p.FullName,
                   Email = p.Email,
                   PhoneNumber = p.PhoneNumber,
                   Address = p.Address,
                   UserUrl = p.UserUrl,
                   CreaetDate = p.CreaetDate,
                   Status = p.Status

               })
               .ToListAsync();

            return parent;
        }

        public Task<List<ProductVm>> GetAllPaging(GetPublicProductPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ParentVm> GetById(int parentId)
        {
            var parent = await _context.Parents.FindAsync(parentId);
            if (parent == null)
            {
                return null;
            }

            var parentVm = new ParentVm
            {
                ParentId = parent.ParentId,
                UserName = parent.UserName,
                Password = parent.Password,
                FullName = parent.FullName,
                Email = parent.Email,
                PhoneNumber = parent.PhoneNumber,
                Address = parent.Address,
                UserUrl = parent.UserUrl,
                CreaetDate = parent.CreaetDate,
                Status = parent.Status

            };

            return parentVm;
        }

        public async Task<int> Update(ParentUpdateRequest request)
        {
            var parent = await _context.Parents.FindAsync(request.ParentId);
            if (parent == null)
            {
                throw new Exception($"Cannot find a product with id:{request.ParentId}.");
            }

            parent.Password = request.Password;
            parent.FullName = request.FullName;
            parent.PhoneNumber = request.PhoneNumber;
            parent.Status = request.Status;
            parent.Address = request.Address;
            parent.UserUrl = request.UserUrl;

            return await _context.SaveChangesAsync();
        }
    }
}
