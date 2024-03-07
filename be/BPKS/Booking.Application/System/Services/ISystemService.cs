using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.ProductType;
using BookingSolution.ViewModels.System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.System.Services
{
    public interface ISystemService 
    {
        Task<List<PartyVm>> GetPartyInPending();
        Task<int> UpdatePartyStatus(PartyStatusUpdateRequest request);
        Task<List<PartyVm>> GetPartyInActive();
        // Task<PartyUserView> GetPartyDetail(PartyStatusUpdateRequest request);
        // Task<List<PartyUserView>> GetAllPartyDetail();
        Task<List<PartyHistory>> PartyHistory(PartyHistoryRequest request);
        Task<int> UserOrderParty(PartyStatusUpdateRequest request);
        Task<int> UserFeedBack(PeedBackPartyRequest request);
        Task<List<ProductQuantityView>> OrderProductQuanity(List<ProductlistRequest> request);
    }
}
