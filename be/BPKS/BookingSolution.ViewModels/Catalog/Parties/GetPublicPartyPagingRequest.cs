using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parties
{
    public class GetPublicPartyPagingRequest : PagingRequestBase
    {
        public int CategoryId { get; set; }
    }
}
