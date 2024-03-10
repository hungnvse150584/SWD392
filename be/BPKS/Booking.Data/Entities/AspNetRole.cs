using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Booking.Data.Entities
{

    public class AspNetRole : IdentityRole<Guid>
    {
        public const string Admin = "admin";
        public const string PartyHost = "partyhost";
        public const string Parent = "parent";
    }
}