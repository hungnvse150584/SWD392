using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;

namespace Booking.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetPagings(GetManageProductPagingRequest request);

        Task<PagedResult<ProductVm>> GetPagingsParentParty(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, BookingSolution.ViewModels.Catalog.Products.CategoryAssignRequest request);

        Task<ProductVm> GetById(int id);

        //NhậnSản phẩm nổi bật
        Task<List<ProductVm>> GetFeaturedProducts(int take);

        //Nhận sản phẩm mới nhất
        Task<List<ProductVm>> GetLatestProducts(int take);

        Task<bool> DeleteProduct(int id);
        Task<bool> UpdateQuantity(AddProductRequest request);
    }
}
