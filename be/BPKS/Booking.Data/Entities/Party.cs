using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

public partial class Party
{
    public int PartyId { get; set; }

    public int? PartyHostId { get; set; }

    public string? PartyName { get; set; }

    public string? Description { get; set; }

    public string? PhoneContact { get; set; }

    public string? Place { get; set; }

    public double? Rate { get; set; }

    public string? ThumbnailUrl { get; set; }

    public DateOnly? DayStart { get; set; }

    public DateOnly? DayEnd { get; set; }

    public string? PartyStatus { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ListParty> ListParties { get; set; } = new List<ListParty>();

    public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();

    public virtual ICollection<ListRoom> ListRooms { get; set; } = new List<ListRoom>();
}
