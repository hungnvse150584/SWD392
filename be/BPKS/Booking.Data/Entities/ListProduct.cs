using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

public partial class ListProduct
{
    public int ListProductId { get; set; }

    public int? PartyId { get; set; }

    public int? RoomId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public string? ListProductStatus { get; set; }

    public virtual Party? Party { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Room? Room { get; set; }
}
