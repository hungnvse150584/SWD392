using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/ListRoom.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/ListRoom.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/ListRoom.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/ListRoom.cs

public partial class ListRoom
{
    public int ListRoomId { get; set; }

    public int? PartyId { get; set; }

    public int? ParentId { get; set; }

    public int? RoomId { get; set; }

    public string? ListRoomStatus { get; set; }

    public virtual Party? Party { get; set; }

    public virtual Room? Room { get; set; }
}
