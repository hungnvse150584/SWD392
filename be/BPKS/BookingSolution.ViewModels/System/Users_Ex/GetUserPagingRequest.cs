using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.System.Users_Ex
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}