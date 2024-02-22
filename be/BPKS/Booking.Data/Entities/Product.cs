using Booking.Data.Enities;
using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/Product.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/Product.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/Product.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/Product.cs

public partial class Product
{
    public int ProductId { get; set; }

    public int? PartyHostId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductUrl { get; set; }

    public string? ProductType { get; set; }

    public string? ProductStyle { get; set; }

    public double? Price { get; set; }

    public string? ProductStatus { get; set; }

    public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();
}
