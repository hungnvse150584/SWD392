using Booking.Application.Catalog.Parties.Dtos.Public;
using Booking.Application.Dtos;

namespace Booking.Application.Catalog.Parties.Dtos.Manage
{
    public interface IPublicPartyService
    {
        PageViewModel<PartyViewModel> GetAllCategoryId(GetPartyPagingRequest request);
    }
}
