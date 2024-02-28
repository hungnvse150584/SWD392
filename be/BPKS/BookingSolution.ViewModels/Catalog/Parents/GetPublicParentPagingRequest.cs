using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parents
{
    public class GetPublicParentPagingRequest : PagingRequestBase
    {
        public string? Status { get; set; }
    }
}
