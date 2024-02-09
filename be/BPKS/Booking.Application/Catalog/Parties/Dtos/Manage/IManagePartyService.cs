using Booking.Application.Catalog.Parties.Dtos;
using Booking.Application.Dtos;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parties.Dtos.Manage
{
    public interface IManagePartyService
    {
        Task<int> Create(PartyCreateRequest request);

        Task<int> Update(PartyUpdateRequest request);

        Task<int> Delete(int productId);

        Task<List<PartyViewModel>> GetAll();

        Task<PagedResult<PartyViewModel>> GetAllPaging(GetManagePartyPagingRequest request);
    }
}
