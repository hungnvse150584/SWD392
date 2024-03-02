using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public int? PartyHostId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductUrl { get; set; }

    public int? ProductType { get; set; }

    public string? ProductStyle { get; set; }

    public double? Price { get; set; }

    public string? ProductStatus { get; set; }

    public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();
}
