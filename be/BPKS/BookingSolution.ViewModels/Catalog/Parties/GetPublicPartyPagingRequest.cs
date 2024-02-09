using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parties.Dtos.Public
{
    public class GetPublicPartyPagingRequest : PagingRequestBase
    {
        public int CategoryId { get; set; }
    }
}
