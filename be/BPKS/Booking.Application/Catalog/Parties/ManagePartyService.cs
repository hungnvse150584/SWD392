using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using DocumentFormat.OpenXml.InkML;
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
                    Place =     x.p.Place,
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
    }
}
