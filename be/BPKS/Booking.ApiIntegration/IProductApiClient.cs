using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;

namespace Booking.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetPagings(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<ProductVm> GetById(int id);

        //NhậnSản phẩm nổi bật
        Task<List<ProductVm>> GetFeaturedProducts(int take);

        //Nhận sản phẩm mới nhất
        Task<List<ProductVm>> GetLatestProducts(int take);

        Task<bool> DeleteProduct(int id);
    }
}
