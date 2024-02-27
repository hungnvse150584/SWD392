using Booking.Application.Catalog.Rooms;
using Booking.Data.EF;

using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Rooms
{
    public class PublicRoomService : IPublicRoomService
    {
        private readonly BookingDbContext _context;
        public PublicRoomService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomVm>> GetAll()
        {
            // Truy vấn tất cả các sản phẩm từ cơ sở dữ liệu
            var rooms = await _context.Rooms.ToListAsync();

            // Chuyển đổi danh sách sản phẩm sang đối tượng ProductVm
            var roomVms = rooms.Select(r => new RoomVm
            {
                RoomId = r.RoomId,
                PartyId = r.PartyId,
                RoomName = r.RoomName,
                RoomUrl = r.RoomUrl,
                RoomType = r.RoomType,
                Price = r.Price,
                RoomStatus = r.RoomStatus
                // Các thuộc tính khác của ProductVm
            }).ToList();

            return roomVms;
        }

        //test
        public Task<PagedResult<RoomVm>> GetAllByStyle(GetPublicRoomPagingRequest request)
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
