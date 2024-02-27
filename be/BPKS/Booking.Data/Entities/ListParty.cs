using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

public partial class ListParty
{
    public int ListPartyId { get; set; }

    public int? ParentId { get; set; }

    public int? PartyId { get; set; }

    public int? PartyHostId { get; set; }

    public string? ListPartyStatus { get; set; }

    public virtual Party? Party { get; set; }
}
