using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Rooms
{
    public class GetPublicRoomPagingRequest : PagingRequestBase
    {
        public string? RoomName { get; set; }
        public string? RoomType { get; set; }
        

    }
}