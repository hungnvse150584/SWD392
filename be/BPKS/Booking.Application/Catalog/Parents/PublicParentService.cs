using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.ViewModels.Catalog.Parents;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parents
{
    public class PublicParentService : IPublicParentService
    {
        private readonly BookingDBContext _context;
        public PublicParentService(BookingDBContext context)
        {
            _context = context;
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
        public Task<PagedResult<ParentVm>> GetAllByStatus(GetPublicParentPagingRequest request)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Parent>> GetParentsSortedByNameAsync(string sortOrder)
        {
            var parents = _context.Parents.AsQueryable();

            if (sortOrder == "desc")
            {
                parents = parents.OrderByDescending(p => p.FullName);
            }
            else
            {
                parents = parents.OrderBy(p => p.FullName);
            }

            return await parents.ToListAsync();
        }

        Task<IEnumerable<ParentVm>> IPublicParentService.GetParentsSortedByNameAsync(string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
