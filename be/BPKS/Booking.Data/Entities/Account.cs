using System;
using System.Collections.Generic;

<<<<<<< Updated upstream:be/BPKS/Booking.Data/Enities/Account.cs
namespace Booking.Data.Enities;
=======
<<<<<<< HEAD:be/BPKS/Booking.Data/Entities/Account.cs
namespace Booking.Data.Entities;
=======
namespace Booking.Data.Enities;
>>>>>>> d881a6cbe332f76d45828e55c578ac0177c81aa3:be/BPKS/Booking.Data/Enities/Account.cs
>>>>>>> Stashed changes:be/BPKS/Booking.Data/Entities/Account.cs

public partial class Account
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? AvatarUrl { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public string? Address { get; set; }

    public int? Role { get; set; }

    public string? Status { get; set; }
}
