using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;

namespace Booking.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductVm>> GetAllByStyle(GetPublicProductPagingRequest request);

        Task<List<ProductVm>> GetAll();
    }
}
