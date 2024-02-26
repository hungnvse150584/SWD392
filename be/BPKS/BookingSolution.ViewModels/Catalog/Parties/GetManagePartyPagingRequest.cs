using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parties
{
    public class GetManagePartyPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
