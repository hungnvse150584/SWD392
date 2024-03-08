using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Rooms
{
    public class ManageRoomService : IManageRoomService
    {
        private readonly BookingDbContext _context;
        private static string Bucket = "bpks-ee4a1.appspot.com";
        public ManageRoomService(BookingDbContext context)
        {
            _context = context;
        }

        public Task<int> AddProduct(AddProductRequest request)
        {
            var query =
                from lr in _context.ListRooms
                join r in _context.Rooms on lr.RoomId equals r.RoomId
                join p in _context.Parties on lr.PartyId equals p.PartyId
                select new { r, p };

            query.Select(x => x.r.RoomId == request.RoomId);
            query.Select(x => x.p.PartyId == request.PartyId);

            if (query.Count() > 0)
            {
                foreach (var item in request.listproducts)
                {
                    var product = new ListProduct
                    {
                        ListProductStatus = "active",
                        ProductId = item.ProductID,
                        PartyId = request.PartyId,
                        RoomId = request.RoomId,
                    };
                    _context.ListProducts.Add(product);
                }
            }
            _context.SaveChanges();
            return query.CountAsync();

        }
        public async Task<int> Create(RoomCreateRequest request)
        {
            var listproducts = request.ProductIds;

            var room = new Room()
            { 
                RoomName = request.RoomName,
                RoomUrl = request.RoomUrl != null? await this.SaveFile( request.RoomUrl):"",
                RoomType = request.RoomType,
                Price = request.Price,
                RoomStatus = "Pending"
           
            };
            _context.Rooms.Add(room);
            _context.SaveChanges();
            var roomrequest = _context.Rooms.FirstOrDefault(r => r.RoomUrl == room.RoomUrl);
            foreach (var listProduct in listproducts)
            {
                var add = new ListProduct { 
                ListProductStatus = "Active",
                PartyId = request.PartyId,
                ProductId = listProduct,
                RoomId = roomrequest.RoomId,
                Quantity = 0
                }; 
                _context.ListProducts.Add(add);
            }

            var listroom = new ListRoom
            {
                PartyId = request.PartyId,
                RoomId = roomrequest.RoomId,
                ListRoomStatus = "Active"
            };
            _context.ListRooms.Add(listroom);

            await _context.SaveChangesAsync();
            return roomrequest.RoomId;
        }
        public async Task<int> Update(RoomUpdateRequest request)
        {
            var room = await _context.Rooms.FindAsync(request.RoomtId);
            if (room == null)
            {
                throw new Exception($"Cannot find a Room with id:{request.RoomtId}.");
            }
            
            room.RoomName = request.RoomName;
            room.RoomUrl = request.RoomUrl != null?  await this.SaveFile(request.RoomUrl) : "";
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
                    RoomName = r.RoomName,
                    RoomUrl = r.RoomUrl,
                    RoomType = r.RoomType,
                    Price = r.Price,
                    RoomStatus = r.RoomStatus
                })
                .ToListAsync();

            return room;
        }

        public async Task<PagedResult<RoomVm>> GetAllPaging(GetPublicRoomPagingRequest request)
        {
            var query =
                  from r in _context.Rooms
                  select new { r };

            //2. filter
            if (!string.IsNullOrEmpty(request.RoomName))
                query = query.Where(x => x.r.RoomName.Contains(request.RoomName));

            if (!string.IsNullOrEmpty(request.RoomType))
                query = query.Where(x => x.r.RoomType.Contains(request.RoomType));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RoomVm()
                {
                    RoomId = x.r.RoomId,
                    RoomName = x.r.RoomName,
                    RoomUrl = x.r.RoomUrl,
                    RoomType = x.r.RoomType,
                    Price = x.r.Price,
                    RoomStatus = x.r.RoomStatus

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<RoomVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;

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
                
                RoomName = r.RoomName,
                RoomUrl = r.RoomUrl,
                RoomType = r.RoomType,
                Price = r.Price,
                RoomStatus = r.RoomStatus
            };

            return roomVm;
        }

        private async Task<string> SaveFile(IFormFile file)
        {

            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var task = new FirebaseStorage(Bucket,
               new FirebaseStorageOptions
               {
                   ThrowOnCancel = true
               })
               .Child("images")
               .Child("RoomImage")
               .Child(fileName)
               .PutAsync(file.OpenReadStream());
            return await task;
        }

        
    }
}
