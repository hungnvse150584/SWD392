using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<RoomVm>> GetAllByStyle(GetPublicProductPagingRequest request);

        Task<List<RoomVm>> GetAll();
    }
}
