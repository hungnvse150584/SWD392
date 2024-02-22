using Booking.Application.Catalog.Parties.Dtos;
using BookingSolution.ViewModels.Catalog.Products;
using Booking.Application.Catalog.Parties.Dtos;

namespace Booking.Application.Catalog.Parties
{
    public interface IPublicPartyService
    {
        PageViewModel<PartyVm> GetAllCategoryId(GetManagePartyPagingRequest request);
    }
}
