using BookingSolution.ViewModels.Catalog.Products;

namespace Booking.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(RoomCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<List<RoomVm>> GetAll();
        Task<List<RoomVm>> GetAllPaging(GetPublicProductPagingRequest request);
        Task<RoomVm> GetById(int productId);
    }
}
