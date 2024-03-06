using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Services
{
    public class UserOrderParty
    {
        public Guid ParentId { get; set; }
        public int RoomId { get; set; }
    }
}
