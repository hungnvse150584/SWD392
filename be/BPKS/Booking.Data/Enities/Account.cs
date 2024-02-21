﻿using System;
using System.Collections.Generic;

namespace z.Enities;

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
