using BookingSolution.ViewModels.Catalog.Products;

namespace Booking.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<List<ProductVm>> GetAll();
        Task<List<ProductVm>> GetAllPaging(GetPublicProductPagingRequest request);
    }
}
