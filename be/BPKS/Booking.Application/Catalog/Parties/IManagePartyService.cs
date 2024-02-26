using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Parties
{
    public interface IManagePartyService
    {
        Task<int> Create(PartyCreateRequest request);

        Task<int> Update(PartyUpdateRequest request);

        Task<int> Delete(int partyId);

        Task<List<PartyVm>> GetAll();

        Task<PagedResult<PartyVm>> GetAllPaging(GetManagePartyPagingRequest request);
    }
}
