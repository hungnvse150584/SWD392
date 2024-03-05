using BookingSolution.ViewModels.Catalog.Parties;

using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Parties
{
    public interface IManagePartyService
    {
        Task<int> Create(PartyCreateRequest request);

        Task<int> Update(PartyUpdateRequest request);

        Task<int> Delete(int partyId);

        Task<PartyVm> GetById(int roomId);
        Task<List<PartyVm>> GetAll();

        Task<PagedResult<PartyVm>> GetPartyNamePaging(GetManagePartyPagingRequest request);

        Task<PagedResult<PartyVm>> GetPartyNameNPartyHostPaging(GetPartyNameNPartyHostPaging request);

        Task<PagedResult<PartyVm>> GetAllPaging(GetPublicPartyPagingRequest request);
    }
}
