using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Services
{
    public class PartyHistoryRequest
    {
        public Guid? user {  get; set; }
        public string status { get; set; }
    }
}
