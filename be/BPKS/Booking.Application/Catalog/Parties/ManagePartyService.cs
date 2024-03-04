using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Catalog.Parties
{
    public class ManagePartyService : IManagePartyService
    {
        private readonly BookingDbContext _context;
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
                //ThumbnailUrl = request.ThumbnailUrl,
                //DayStart = request.DayStart,
                //DayEnd = request.DayEnd,
                //CreatedDate = DateTime.Now,
                //PartyStatus = request.PartyStatus,
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
                    PartyName = p.PartyName,
                    Description = p.Description,
                    PhoneContact = p.PhoneContact,
                    Place = p.Place,
                    Rate = (double)p.Rate,
                    ThumbnailUrl = p.ThumbnailUrl,
                    PartyStatus = p.PartyStatus,
                    DayStart = (DateOnly)p.DayStart,
                    DayEnd = (DateOnly)p.DayEnd,
                    CreatedDate = (DateOnly)p.CreatedDate
                })
                .ToListAsync();

            return party;
        }

        public Task<PagedResult<PartyVm>> GetAllPaging(GetManagePartyPagingRequest request)
        {
            throw new NotImplementedException();
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
            party.ThumbnailUrl = request.ThumbnailUrl;
            party.DayEnd = request.DayEnd;
            party.PartyStatus = request.PartyStatus;
            
            //product.ProductName = request.ProductName;
            //product.ProductUrl = await this.SaveFile(request.ThumbnailImage);
            //product.ProductType = request.ProductType;
            //product.ProductStyle = request.ProductStyle;
            //product.Price = request.Price;



            return await _context.SaveChangesAsync();
        }

    }
}
