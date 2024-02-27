using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Rooms
{
    public class GetPublicRoomPagingRequest : PagingRequestBase
    {
        public string? RoomStyle { get; set; }

    }
}