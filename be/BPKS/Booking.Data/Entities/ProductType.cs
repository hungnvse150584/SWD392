using System;
using System.Collections.Generic;

namespace Booking.Data.Entities;

public partial class ProductType
{
    public int Id { get; set; }

    public string? ProductTypeName { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
