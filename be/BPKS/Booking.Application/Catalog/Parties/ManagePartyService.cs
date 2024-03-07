using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net.Sockets;

namespace Booking.Application.Catalog.Parties
{
    public class ManagePartyService : IManagePartyService
    {
        private readonly BookingDbContext _context;
        private static string Bucket = "bpks-ee4a1.appspot.com";
        public ManagePartyService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(PartyCreateRequest request)
        {
            var party = new Party()
            {
                PartyName = request.PartyName,
                Description = request.Description,
                PhoneContact = request.PhoneContact,
                Place = request.Place,
                Rate = 0,
                CreatedDate = DateTime.Now,
                DayStart = request.DayStart,
                DayEnd = request.DayEnd,
                PartyHostId = request.PartyHostId,
                PartyStatus = "Active",
                ThumbnailUrl = await this.SaveFile(request.ThumbnailUrl),

            };
            _context.Parties.Add(party);
            _context.SaveChanges();

            var partyrequest = _context.Parties.FirstOrDefault(p => p.ThumbnailUrl == party.ThumbnailUrl);

            if (partyrequest != null)
            {
                var listparty = new ListParty
                {
                    PartyHostId = request.PartyHostId,
                    PartyId = partyrequest?.PartyId,
                };

                _context.ListParties.Add(listparty);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int partyId)
        {
            var party = await _context.Parties.FindAsync(partyId);
            if (party != null) throw new BookingException($"Cannot find a party: {partyId}");
            else
            {
                _context.Parties.Remove(party);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<PartyVm>> GetAll()
        {
            var party = await _context.Parties
                .Select(p => new PartyVm
                {
                    PartyId = p.PartyId,
                    PartyHostId = p.PartyHostId,
                    PartyName = p.PartyName,
                    Description = p.Description,
                    PhoneContact = p.PhoneContact,
                    Place = p.Place,
                    Rate = p.Rate,
                    ThumbnailUrl = p.ThumbnailUrl,
                    PartyStatus = p.PartyStatus,
                    DayStart = p.DayStart,
                    DayEnd = p.DayEnd,
                    CreatedDate = p.CreatedDate
                })
                .ToListAsync();

            return party;
        }
        public async Task<List<PartyHistory>> PartyHostHistory(PartyHistoryRequest request)
        {
            var query =
                from u in _context.AspNetUsers
                //join lparent in _context.ListParties on u.Id equals lparent.ParentId
                join lpartyhost in _context.ListParties on u.Id equals lpartyhost.PartyHostId
                join p in _context.Parties on lpartyhost.PartyId equals p.PartyId
                where u.Id == request.user
                select new { u, p };

            var data = query.Select(t => new PartyHistory
            {
                CreatedDate = t.p.CreatedDate,
                DayEnd = t.p.DayEnd,
                DayStart=t.p.DayStart,
                Description = t.p.Description,
                PartyName = t.p.PartyName,
                PhoneContact = t.p.PhoneContact,
                Place = t.p.Place,
                ThumbnailUrl = t.p.ThumbnailUrl,
                Rate = t.p.Rate,
                
            }).ToList();

           return data;
        }
        public async Task<List<PartyHistory>> ParentHistory(PartyHistoryRequest request)
        {
            var query =
                from u in _context.AspNetUsers
                join lparent in _context.ListParties on u.Id equals lparent.ParentId
                //join lpartyhost in _context.ListParties on u.Id equals lpartyhost.PartyHostId
                join p in _context.Parties on lparent.PartyId equals p.PartyId
                where u.Id == request.user
                select new { u, p };

            var data = query.Select(t => new PartyHistory
            {
                CreatedDate = t.p.CreatedDate,
                DayEnd = t.p.DayEnd,
                DayStart = t.p.DayStart,
                Description = t.p.Description,
                PartyName = t.p.PartyName,
                PhoneContact = t.p.PhoneContact,
                Place = t.p.Place,
                ThumbnailUrl = t.p.ThumbnailUrl,
                Rate = t.p.Rate,

            }).ToList();

            return data;
        }
        public async Task<PagedResult<PartyVm>> GetAllPaging(GetPublicPartyPagingRequest request)
        {
            var query =
                from p in _context.Parties
                select new { p };

            //2. filter
            if (!string.IsNullOrEmpty(request.PartyName))
                query = query.Where(x => x.p.PartyName.Contains(request.PartyName));
            if (request.PartyHostId != null)
                query = query.Where(x => x.p.PartyHostId == request.PartyHostId);

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PartyVm()
                {
                    PartyId = x.p.PartyId,
                    PartyHostId = x.p.PartyHostId,
                    PartyName = x.p.PartyName,
                    Description = x.p.Description,
                    PhoneContact = x.p.PhoneContact,
                    Place = x.p.Place,
                    Rate = x.p.Rate,
                    ThumbnailUrl = x.p.ThumbnailUrl,
                    PartyStatus = x.p.PartyStatus,
                    DayStart = x.p.DayStart,
                    DayEnd = x.p.DayEnd,
                    CreatedDate = x.p.CreatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<PartyVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<PartyVm>> GetPartyNamePaging(GetManagePartyPagingRequest request)
        {
            var query =
                from p in _context.Parties
                select new { p };

            //2. filter
            if (!string.IsNullOrEmpty(request.keyword))
                query = query.Where(x => x.p.PartyName.Contains(request.keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PartyVm()
                {
                    PartyId = x.p.PartyId,
                    PartyHostId = x.p.PartyHostId,
                    PartyName = x.p.PartyName,
                    Description = x.p.Description,
                    PhoneContact = x.p.PhoneContact,
                    Place = x.p.Place,
                    Rate = x.p.Rate,
                    ThumbnailUrl = x.p.ThumbnailUrl,
                    PartyStatus = x.p.PartyStatus,
                    DayStart = x.p.DayStart,
                    DayEnd = x.p.DayEnd,
                    CreatedDate = x.p.CreatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<PartyVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<PartyVm>> GetPartyNameNPartyHostPaging(GetPartyNameNPartyHostPaging request)
        {
            var query =
                from p in _context.Parties
                select new { p };

            //2. filter
            if (request.partyhost != Guid.Empty)
                query = query.Where(x => x.p.PartyHostId == request.partyhost);

            if (!string.IsNullOrEmpty(request.keyword))
                query = query.Where(x => x.p.PartyName.Contains(request.keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PartyVm()
                {
                    PartyId = x.p.PartyId,
                    PartyHostId = x.p.PartyHostId,
                    PartyName = x.p.PartyName,
                    Description = x.p.Description,
                    PhoneContact = x.p.PhoneContact,
                    Place = x.p.Place,
                    Rate = x.p.Rate,
                    ThumbnailUrl = x.p.ThumbnailUrl,
                    PartyStatus = x.p.PartyStatus,
                    DayStart = x.p.DayStart,
                    DayEnd = x.p.DayEnd,
                    CreatedDate = x.p.CreatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<PartyVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> Update(PartyUpdateRequest request)
        {
            var party = await _context.Parties.FindAsync(request.PartyId);
            if (party == null)
            {
                throw new Exception($"Cannot find a party with id:{request.PartyId}.");
            }
            party.PartyName = request.PartyName;
            party.Description = request.Description;
            party.PhoneContact = request.PhoneContact;
            party.Place = request.Place;
            party.ThumbnailUrl = await this.SaveFile(request.ThumbnailUrl);
            party.DayEnd = request.DayEnd;
            party.PartyStatus = request.PartyStatus;

            return await _context.SaveChangesAsync();
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
               .Child("PartyImage")
               .Child(fileName)
               .PutAsync(file.OpenReadStream());
            return await task;
        }

        public async Task<PartyVm> GetById(int partyId)
        {
            var p = await _context.Parties.FindAsync(partyId);
            if (p == null)
            {
                return null;
            }

            var roomVm = new PartyVm
            {
                PartyId = p.PartyId,
                PartyHostId = p.PartyHostId,
                PartyName = p.PartyName,
                Description = p.Description,
                PhoneContact = p.PhoneContact,
                Place = p.Place,
                Rate = p.Rate,
                ThumbnailUrl = p.ThumbnailUrl,
                PartyStatus = p.PartyStatus,
                DayStart = p.DayStart,
                DayEnd = p.DayEnd,
                CreatedDate = p.CreatedDate
            };

            return roomVm;
        }

        public async Task<PartyUserView> GetPartyDetail(int partyid)
        {
            PartyUserView partydetail = new PartyUserView();
            var query =
                 from party in _context.Parties
                 join listroom in _context.ListRooms
                 on party.PartyId equals listroom.PartyId
                 join room in _context.Rooms
                 on listroom.RoomId equals room.RoomId
                 join listproduct in _context.ListProducts
                 on room.RoomId equals listproduct.RoomId
                 join product in _context.Products
                 on listproduct.ProductId equals product.ProductId
                 where party.PartyId == partyid
                 orderby party.PartyId ascending, room.RoomId ascending, product.ProductId
                 select new { party, room, product };
            //var data = await query.Select(t =>
            //partydetail.PartyId != t.party.PartyId ? partydetail.PartyId = t.party.PartyId
            //: "",
            //partydetail = t.

            //).ToListAsync(); 
                foreach (var item in query)
            {
                if(item.party.PartyId != partydetail.PartyId)
                {
                    partydetail.PartyId = item.party.PartyId;
                    partydetail.PartyName = item.party.PartyName;
                    partydetail.DayStart = item.party.DayStart;
                    partydetail.DayEnd = item.party.DayEnd;
                    partydetail.PhoneContact = item.party.PhoneContact;
                    partydetail.Description = item.party.Description;
                    partydetail.Place = item.party.Place;
                    partydetail.Rate = item.party.Rate;
                    partydetail.ThumbnailUrl = item.party.ThumbnailUrl;
                    partydetail.roomUserViews = new List<RoomUserView>();
                    partydetail.roomUserViews.Add(new RoomUserView
                    {
                        RoomId = item.room.RoomId,
                        Price = item.room.RoomId,
                        RoomName = item.room.RoomName,
                        RoomUrl = item.room.RoomUrl,
                        RoomType = item.room.RoomType,
                        
                    });
                }
                if(item.room.RoomId != partydetail.roomUserViews.Last().RoomId)
                {
                    var room = new RoomUserView
                    {
                        RoomId = item.room.RoomId,
                        Price = item.room.RoomId,
                        RoomName = item.room.RoomName,
                        RoomUrl = item.room.RoomUrl,
                        RoomType = item.room.RoomType,
                        productUserViews = new List<ProductUserView>(),
                    };
                    partydetail.roomUserViews.Add(room);
                }
                if(item.room.RoomId == partydetail.roomUserViews.Last().RoomId)
                {
                    partydetail.roomUserViews.Last().productUserViews = new List<ProductUserView>();
                    var product = new ProductUserView
                    {
                        ProductId = item.product.ProductId,
                        ProductName = item.product.ProductName,
                        ProductUrl = item.product.ProductUrl,
                        ProductType = item.product.ProductType,
                        Description = item.product.Description,
                        Price = item.product.Price,
                        ProductStyle = item.product.ProductStyle
                    };
                    partydetail.roomUserViews.Last().productUserViews.Add(product);
                }
                if(item.product.ProductId != partydetail.roomUserViews.Last().productUserViews.Last().ProductId)
                {
                    var product = new ProductUserView
                    {
                        ProductId = item.product.ProductId,
                        Description = item.product.Description,
                        Price = item.product.Price,
                        ProductName = item.product.ProductName,
                        ProductUrl = item.product.ProductUrl,
                        ProductType = item.product.ProductType,
                        ProductStyle = item.product.ProductStyle,

                    };
                    partydetail.roomUserViews.Last().productUserViews.Add(product);
                }
                


            }

          return partydetail;

        }

        public  Task<List<PartyVm>> GetPartyWithStatus(GetPartyWithStatus request)
        {
            var query =
                from p in _context.Parties
                join lp in _context.ListParties on p.PartyHostId equals lp.PartyHostId
                where p.PartyStatus == request.Status
                select new {p,lp };

            if (request.Id != Guid.Empty)
                query = query.Where(x => x.lp.PartyHostId == request.Id);
            var data = query.Select(x => new PartyVm()
            {
                PartyId = x.p.PartyId,
                PartyHostId = x.p.PartyHostId,
                PartyName = x.p.PartyName,
                Description = x.p.Description,
                PhoneContact = x.p.PhoneContact,
                Place = x.p.Place,
                Rate = x.p.Rate,
                ThumbnailUrl = x.p.ThumbnailUrl,
                PartyStatus = x.p.PartyStatus,
                DayStart = x.p.DayStart,
                DayEnd = x.p.DayEnd,
                CreatedDate = x.p.CreatedDate

            }).ToListAsync();

            return data;

        }

        public async Task<int> UpdatePartyStatus(UpdatePartyStatusRequest request)
        {
            var party = await _context.Parties.FindAsync(request.PartyId);
            if (party == null)
            {
                throw new Exception($"Cannot find a party with id:{request.PartyId}.");
            }
            party.PartyStatus = request.Status;

            return await _context.SaveChangesAsync();
        }
    }
}
