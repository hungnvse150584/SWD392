using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Rooms
{
    public class GetRoomNamePaging : PagingRequestBase
    {
        public string keyword { get; set; }
    }
}
