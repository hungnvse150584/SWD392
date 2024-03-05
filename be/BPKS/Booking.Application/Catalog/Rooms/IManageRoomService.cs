using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Rooms
{
    public interface IManageRoomService
    {
        Task<int> Create(RoomCreateRequest request);
        Task<int> Update(RoomUpdateRequest request);
        Task<int> Delete(int roomId);
        Task<List<RoomVm>> GetAll();
        Task<PagedResult<RoomVm>> GetAllPaging(GetPublicRoomPagingRequest request);
        Task<RoomVm> GetById(int roomId);
    }
}
