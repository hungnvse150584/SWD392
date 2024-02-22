using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/ListProduct.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/ListProduct.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/ListProduct.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/ListProduct.cs

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
