using System;
using System.Collections.Generic;
namespace Booking.Data.Entities;

public partial class AppConfig
{
    public string Key { get; set; } = null!;

    public string? Value { get; set; }
}
