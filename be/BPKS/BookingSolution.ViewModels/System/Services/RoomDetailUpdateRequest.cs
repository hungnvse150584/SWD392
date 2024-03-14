using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Services
{
    public class RoomDetailUpdateRequest
    {
        public int RoomId { get; set; }

        public string? RoomName { get; set; }

        public IFormFile? RoomUrl { get; set; }

        public string? RoomType { get; set; }

        public double? Price { get; set; }

        //public string? RoomStatus { get; set; }
        public List<ProductDetailsUpdateRequest> productUserViews { get; set; }
    }
}
