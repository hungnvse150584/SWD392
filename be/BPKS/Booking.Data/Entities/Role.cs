using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Booking.Data.Entities
{

    public partial class Role
    {
        public int RoleId { get; set; }

        public string? RoleName { get; set; }


    }
}