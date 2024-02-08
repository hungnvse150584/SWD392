using Booking.Application.Catalog.Parties.Dtos;
using Booking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parties
{
    public interface IPublicPartyService
    {
        PageViewModel<ProductViewModel> GetAllCategoryId(int categoryId, int pageIndex, int pageSize);
    }
}
