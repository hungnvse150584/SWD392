using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;

namespace Booking.ApiIntegration
{
    public interface IRoomApiClient
    {
        Task<PagedResult<RoomVm>> GetPagings(GetManageRoomPagingRequest request);

        Task<PagedResult<RoomVm>> GetPagingsParentRoom(GetManageRoomPagingRequest request);

        Task<bool> CreateRoom(RoomCreateRequest request);

        Task<bool> UpdateRoom(RoomUpdateRequest request);

        //Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<RoomVm> GetById(int id);

        //NhậnSản phẩm nổi bật
        Task<List<RoomUpdateRequest>> GetFeaturedRooms(int take);

        //Nhận sản phẩm mới nhất
        Task<List<RoomVm>> GetLatestRooms(int take);

        Task<bool> DeleteRoom(int id);
        Task<bool> ParentOrder(ParentOrder request);
        Task<bool> AddProducts(AddProductRequest request);
        Task<bool> PartyHostComfirm(PHostComfirmRequest request);
    }
}
