using BookingSolution.ViewModels.Catalog.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.ProductTypes
{
    public interface IProductTypeService
    {
        Task<int> Create(ProductTypeCreateRequest request);
        Task<int> Update(ProductTypeUpdateRequest request);
        Task<int> Delete(int Id);
        Task<List<ProductTypeVm>> GetAll();
        
        Task<ProductTypeVm> GetById(int roomId);
    }
}
