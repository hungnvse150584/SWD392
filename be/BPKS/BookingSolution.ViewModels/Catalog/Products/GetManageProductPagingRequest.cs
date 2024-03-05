using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string? ProductName { get; set; }

        public Guid? PartyHostId { get; set; }

       public int? ProductType { get; set; }

        //public int? CategoryId { get; set; }
    }
}