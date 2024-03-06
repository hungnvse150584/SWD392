using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Services
{
    public class PeedBackPartyRequest
    {
        public Guid parentId { get; set; }

        public int partyId {  get; set; }
        public double score { get; set; }
        public string feedback { get; set; }
    }
}
