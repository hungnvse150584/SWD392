using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Rooms
{
    public class ManageRoomService : IManageRoomService
    {
        private readonly BookingDbContext _context;
        public ManageRoomService(BookingDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(RoomCreateRequest request)
        {
            var room = new Room()
            {
                
                PartyId = request.PartyId,
                RoomName = request.RoomName,
                RoomUrl = request.RoomUrl,
                RoomType = request.RoomType,
               
                Price = request.Price,
                RoomStatus = request.Roomstatus
                
                //ThumbnailUrl = request.ThumbnailUrl,
                //DayStart = request.DayStart,
                //DayEnd = request.DayEnd,
                //CreatedDate = DateTime.Now,
                //PartyStatus = request.PartyStatus,
            };
            _context.Rooms.Add(room);
            return await _context.SaveChangesAsync();
           
        }
        public async Task<int> Update(RoomUpdateRequest request)
        {
            var room = await _context.Rooms.FindAsync(request.RoomtId);
            if (room == null)
            {
                throw new Exception($"Cannot find a Room with id:{request.RoomtId}.");
            }
            room.PartyId = request.PartyId;
            room.RoomName = request.RoomName;
            room.RoomUrl = request.RoomUrl;
            room.RoomType = request.RoomType;
            
            room.Price = request.Price;
            room.RoomStatus = request.RoomStatus;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
            {
                throw new BookingException($"Cannot find a product: {roomId}");
            }

            _context.Rooms.Remove(room);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<RoomVm>> GetAll()
        {
            var room = await _context.Rooms
                .Select(r => new RoomVm
                {
                    RoomId = r.RoomId,
                    PartyId = r.PartyId,
                    RoomName = r.RoomName,
                    RoomUrl = r.RoomUrl,
                    RoomType = r.RoomType,
                    Price = r.Price,
                    RoomStatus = r.RoomStatus
                })
                .ToListAsync();

            return room;
        }

        public Task<List<RoomVm>> GetAllPaging(GetPublicRoomPagingRequest request)
        {
            //var query = from p in _context.Products
            //            join pt in _context.
            throw new NotImplementedException();

        }

        public async Task<RoomVm> GetById(int roomId)
        {
            var r = await _context.Rooms.FindAsync(roomId);
            if (r == null)
            {
                return null;
            }

            var roomVm = new RoomVm
            {
                RoomId = r.RoomId,
                PartyId = r.PartyId,
                RoomName = r.RoomName,
                RoomUrl = r.RoomUrl,
                RoomType = r.RoomType,
                Price = r.Price,
                RoomStatus = r.RoomStatus
            };

            return roomVm;
        }
    }
}
