using Azure.Core;
using Booking.Application.System.Users;
using Booking.Common;
using Booking.Data.EF;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.System.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.System.Services
{
    public class SystemService : ISystemService
    {
        private readonly BookingDbContext _context;

        public SystemService(BookingDbContext context)
        { 
            _context = context;
        }

        public async Task<List<PartyUserView>> GetAllPartyDetail()
        {
            var query =
                from p in _context.Parties
                select new { p };
            var data = await query.Select(q => new PartyUserView
            {
                PartyId = q.p.PartyId,
                PartyName = q.p.PartyName,
                Description = q.p.Description,
                PhoneContact = q.p.PhoneContact,
                Place = q.p.Place,
                Rate = q.p.Rate,
                ThumbnailUrl = q.p.ThumbnailUrl,
                DayStart = q.p.DayStart,
                DayEnd = q.p.DayEnd,

            }
            ).ToListAsync();

            data.ForEach(async dataparty => {
                var listroomquery =
                    from lr in _context.ListRooms
                    join r in _context.Rooms on lr.RoomId equals r.RoomId
                    where lr.PartyId == dataparty.PartyId
                    select new { lr,r };
                var listroom = await listroomquery.Select(lq => new RoomUserView
                {
                    RoomId = lq.r.RoomId,
                    RoomName = lq.r.RoomName,
                    RoomUrl = lq.r.RoomUrl,
                    RoomType = lq.r.RoomType,
                    Price = lq.r.Price,
                } ).ToListAsync();

                

                listroom.ForEach(async dataroom =>
               {
                   var listproductquery = 
                            from lp in _context.ListProducts
                            join p in _context.Products on lp.ProductId equals p.ProductId
                            where lp.ProductId == dataroom.RoomId
                            select new {lp,p};
                   var listproduct = await listproductquery.Select(product => new ProductUserView
                   {
                       ProductName = product.p.ProductName,
                       ProductUrl = product.p.ProductUrl,
                       ProductType = product.p.ProductType,
                       Price = product.p.Price,
                       Description = product.p.Description,
                       ProductStyle = product.p.ProductStyle,
                   }).ToListAsync();

                   dataroom.productUserViews = listproduct;
               });
                dataparty.roomUserViews = listroom;
            });



            return data;
        }

        public Task<PartyUserView> GetPartyDetail(PartyStatusUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartyVm>> GetPartyInActive()
        {
            throw new NotImplementedException();
        }

        public Task<List<PartyVm>> GetPartyInPending()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductQuantityView>> OrderProductQuanity(List<ProductlistRequest> request)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartyHistory>> PartyHistory(PartyHistoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePartyStatus(PartyStatusUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UserFeedBack(PeedBackPartyRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UserOrderParty(PartyStatusUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
