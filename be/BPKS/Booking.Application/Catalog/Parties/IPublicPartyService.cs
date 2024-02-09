using Booking.Application.Catalog.Parties.Dtos;
using BookingSolution.ViewModels.Catalog.Products;

namespace Booking.Application.Catalog.Parties
{
    public interface IPublicPartyService
    {
        PageViewModel<PartyViewModel> GetAllCategoryId(GetManagePartyPagingRequest request);
    }
}
