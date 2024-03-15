using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Parties
{
    public class DetailsRoomBookedRequest
    {
        public Guid? Id { get; set; }
        public int partyId { get; set; }
        public int roomId { get; set; }
    }
}
