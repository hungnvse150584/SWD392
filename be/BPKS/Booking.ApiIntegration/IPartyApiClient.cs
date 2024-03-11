using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApiIntegration
{
    public interface IPartyApiClient
    {
        Task<PagedResult<PartyVm>> GetPagings(GetPublicPartyPagingRequest request);

        Task<bool> CreateParty(PartyCreateRequest request);

        Task<bool> UpdateParty(PartyUpdateRequest request);

        Task<bool> DeleteParty(int id);

        Task<PartyVm> GetById(int id);

        Task<PartyUserView> GetDetails(int id);

    }
}
