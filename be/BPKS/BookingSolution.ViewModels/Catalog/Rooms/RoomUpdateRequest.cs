﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Rooms

{
    public class RoomUpdateRequest
    {
        public int RoomId { get; set; }

        

        public string? RoomName { get; set; }

        //public string? RoomUrl { get; set; }

        public string? RoomType { get; set; }



        public double? Price { get; set; }

        public string? RoomStatus { get; set; }
        public IFormFile? ThumbnailImage { get; set; }

        //public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();

    }
}