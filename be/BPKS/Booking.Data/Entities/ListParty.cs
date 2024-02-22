using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/ListParty.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/ListParty.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/ListParty.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/ListParty.cs

public partial class ListParty
{
    public int ListPartyId { get; set; }

    public int? ParentId { get; set; }

    public int? PartyId { get; set; }

    public int? PartyHostId { get; set; }

    public string? ListPartyStatus { get; set; }

    public virtual Party? Party { get; set; }
}
