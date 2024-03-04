using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<List<ProductVm>> GetAll();
        Task<PagedResult<ProductVm>> GetAllProducType(GetManageProductPagingRequest request);
        Task<ProductVm> GetById(int productId);
        Task<PagedResult<ProductVm>> GetAllByStyle(GetPublicProductPagingRequest request);
    
    
    }
}
