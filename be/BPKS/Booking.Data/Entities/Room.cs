using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/Room.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/Room.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/Room.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/Room.cs

public partial class Room
{
    public int RoomId { get; set; }

    public int? PartyId { get; set; }

    public string? RoomName { get; set; }

    public string? RoomUrl { get; set; }

    public string? RoomType { get; set; }

    public double? Price { get; set; }

    public string? RoomStatus { get; set; }

    public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();

    public virtual ICollection<ListRoom> ListRooms { get; set; } = new List<ListRoom>();
}
