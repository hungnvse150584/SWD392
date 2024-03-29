﻿using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.ProductType;
using BookingSolution.ViewModels.System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.System.Services
{
    public interface ISystemService 
    {
        public Task<double> GetTotalCash();
        public Task<int> GetTotalPartyBooked();
        public Task<int> GetTotalUser();
    }
}
