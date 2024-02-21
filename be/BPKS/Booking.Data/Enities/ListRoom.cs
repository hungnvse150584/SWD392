using System;
using System.Collections.Generic;

namespace z.Enities;

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
