using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;
using BookingSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Application.Catalog.Parties
{
    public interface IManagePartyService
    {
        Task<int> Create(PartyCreateRequest request);

        Task<int> Update(PartyUpdateRequest request);

        Task<int> Delete(int partyId);

        Task<PartyVm> GetById(int roomId);

        Task<List<PartyVm>> GetAll();
        Task<PagedResult<PartyVm>> GetPartyApprove(GetPublicPartyPagingRequest request);
        Task<PagedResult<PartyVm>> GetPartyPartyHostView(GetPublicPartyPagingRequest request);

        Task<PagedResult<PartyVm>> GetAllPaging(GetPublicPartyPagingRequest request);

        Task<List<PartyVm>> GetPartyWithStatus(GetPartyWithStatus request);
        
        Task<int> UpdatePartyStatus(UpdatePartyStatusRequest request);
        Task<int> Approve(int id);
        Task<int> Rejected(int id);

        Task<PartyUserView> GetPartyDetail(int request);

        //Task<List<PartyHistory>> PartyHistory(PartyHistoryRequest request);

        Task<List<PartyHistory>> ParentHistory(PartyHistoryRequest request);

        Task<List<PartyHistory>> PartyHostHistory(PartyHistoryRequest request);

        Task<int> UpdatePartyDetails(PartyDetailsUpdateRequest request);
        Task<int> FeedBack(FeedbackRequest request);

        Task<int> ComfirmParty(int partyId);

        Task<int> CheckOut(int  partyId);
        

        Task<PartyUserView> DetailsRoomBooked(DetailsRoomBookedRequest request);
        //Task<List<RoomVm>> GetRoomsByPartyId(int partyId);

    }
}
