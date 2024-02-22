using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/Party.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/Party.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/Party.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/Party.cs

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

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

    public virtual ICollection<ListParty> ListParties { get; set; } = new List<ListParty>();

    public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();

    public virtual ICollection<ListRoom> ListRooms { get; set; } = new List<ListRoom>();
}
