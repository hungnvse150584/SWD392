using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

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
