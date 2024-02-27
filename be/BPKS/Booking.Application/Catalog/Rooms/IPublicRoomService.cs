using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Rooms
{
    public interface IPublicRoomService
    {
        Task<PagedResult<RoomVm>> GetAllByStyle(GetPublicRoomPagingRequest request);

        Task<List<RoomVm>> GetAll();
    }
}
