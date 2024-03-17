using Azure.Core;
using Booking.Application.System.Users;
using Booking.Common;
using Booking.Data.EF;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.System.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.System.Services
{
    public class SystemService : ISystemService
    {
        private readonly BookingDbContext _context;

        public SystemService(BookingDbContext context)
        { 
            _context = context;
        }

        public async Task<double> GetTotalCash()
        {
            double sum = 0;
            var query = await _context.ListRooms.Where(x=> x.ParentId != null).ToListAsync();
            foreach (var room in query)
            {
                if (room.Total != 0)
                {
                    sum += room.Total;
                }
            }
            return sum;
        }

        public async Task<int> GetTotalPartyBooked()
        {
            var query = await _context.ListRooms.Where(x => 
            x.ParentId != null && x.ListRoomStatus != "Pending" && x.ListRoomStatus != "Approved"
            ).ToListAsync();

            return query.Count;
        }

        public async Task<int> GetTotalUser()
        {
            var query = await _context.AspNetUsers.CountAsync();
            return query;
        }
    }
}
