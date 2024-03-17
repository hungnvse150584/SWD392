using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Users
{
    public class FeedbackRequest
    {
        public Guid? ParentId { get; set; }
        public int? PartyId { get; set; }
        public int? RoomId { get; set; }
        public int? Score { get; set; }
        public string? Feedback { get; set; }
    }
}
