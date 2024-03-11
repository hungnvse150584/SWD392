using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Rooms
{
    public class GetManageRoomPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string? RoomName { get; set; }

        public string? RoomType { get; set; }
        public Guid? PartyHostId { get; set; }
    }
}