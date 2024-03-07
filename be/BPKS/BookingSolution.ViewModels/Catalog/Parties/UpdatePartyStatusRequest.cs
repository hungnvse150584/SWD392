using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parties
{
    public class UpdatePartyStatusRequest
    {
        public int PartyId { get; set; }
        public string Status { get; set;}
    }
}
