using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parties
{
    public class GetPartyNameNPartyHostPaging :PagingRequestBase
    {
        public string? keyword { get; set; }
        public Guid partyhost {  get; set; }
    }
}
