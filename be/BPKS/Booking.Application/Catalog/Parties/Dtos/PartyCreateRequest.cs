using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parties.Dtos
{
    public class PartyCreateRequest
    {
        public string PartyName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
