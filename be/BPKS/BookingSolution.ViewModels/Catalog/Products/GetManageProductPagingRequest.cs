﻿using BookingSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        //public int? CategoryId { get; set; }
    }
}