using BookingSolution.ViewModels.Catalog.Parties;

using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;

namespace Booking.Application.Catalog.Parties
{
    public interface IManagePartyService
    {
        Task<int> Create(PartyCreateRequest request);

        Task<int> Update(PartyUpdateRequest request);

        Task<int> Delete(int partyId);

        Task<PartyVm> GetById(int roomId);
        Task<List<PartyVm>> GetAll();
        Task<List<PartyVm>> GetPartyWithStatus(GetPartyWithStatus request);
        Task<int> UpdatePartyStatus(UpdatePartyStatusRequest request);
        Task<PagedResult<PartyVm>> GetPartyNamePaging(GetManagePartyPagingRequest request);

        Task<PagedResult<PartyVm>> GetPartyNameNPartyHostPaging(GetPartyNameNPartyHostPaging request);

        Task<PagedResult<PartyVm>> GetAllPaging(GetPublicPartyPagingRequest request);
        Task<PartyUserView> GetPartyDetail(int request);

        //Task<List<PartyHistory>> PartyHistory(PartyHistoryRequest request);

        Task<List<PartyHistory>> ParentHistory(PartyHistoryRequest request);
        Task<List<PartyHistory>> PartyHostHistory(PartyHistoryRequest request);



    }
}
