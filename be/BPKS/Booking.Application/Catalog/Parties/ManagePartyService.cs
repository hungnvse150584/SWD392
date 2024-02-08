using Booking.Application.Catalog.Parties.Dtos;
using Booking.Application.Catalog.Party.Dtos;
using Booking.Application.Catalog.Products.Dtos;
using Booking.Application.Dtos;
using BPKS.EF;
using BPKS.Entities;

namespace Booking.Application.Catalog.Parties
{
    public class ManagePartyService : IManagePartyService
    {
        private readonly BookingDBContext _context;
        public ManagePartyService(BookingDBContext context) 
        {
            _context = context;
        }

        public async Task<int> Create(PartyCreateRequest request)
        {
            var party = new Party()
            {
                PartyName = request.PartyName,
            };
            _context.Parties.Add(party);
            await _context.SaveChangesAsync();
        }

        public Task<int> Delete(PartyDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(PartyEditRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
