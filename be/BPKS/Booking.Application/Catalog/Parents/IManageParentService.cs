using BookingSolution.ViewModels.Catalog.Parents;
using BookingSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parents
{
    public interface IManageParentService
    {
        Task<int> Create(ParentCreateRequest request);
        Task<int> Update(ParentUpdateRequest request);
        Task<int> Delete(int parentId);
        Task<List<ParentVm>> GetAll();
        Task<List<ProductVm>> GetAllPaging(GetPublicProductPagingRequest request);
        Task<ParentVm> GetById(int parentId);

    }
}
