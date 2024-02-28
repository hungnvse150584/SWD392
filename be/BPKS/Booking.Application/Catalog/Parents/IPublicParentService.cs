using Booking.Data.Entities;
using BookingSolution.ViewModels.Catalog.Parents;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parents
{
    public interface IPublicParentService
    {
        Task<PagedResult<ParentVm>> GetAllByStatus(GetPublicParentPagingRequest request);

        Task<List<ParentVm>> GetAll();
        Task<IEnumerable<ParentVm>> GetParentsSortedByNameAsync(string sortOrder);
    }
}
