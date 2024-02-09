﻿using Booking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Catalog.Parties.Dtos
{
    public class GetPartyPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
