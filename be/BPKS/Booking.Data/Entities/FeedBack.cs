using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

public partial class Feedback
{
    public int FeedBackId { get; set; }

    public int? ParentId { get; set; }

    public int? PartyId { get; set; }

    public int? PartyHostId { get; set; }

    public int? Score { get; set; }

    public string? Feedback1 { get; set; }

    public virtual Party? Party { get; set; }
}
