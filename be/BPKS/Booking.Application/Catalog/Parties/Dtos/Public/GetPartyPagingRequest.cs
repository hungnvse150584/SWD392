using Booking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parties.Dtos.Public
{
    public class GetPartyPagingRequest : PagingRequestBase
    {
        public int CategoryId { get; set; }
    }
}
