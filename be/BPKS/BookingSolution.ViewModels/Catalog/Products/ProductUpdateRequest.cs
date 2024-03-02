using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int ProductId { get; set; }

        public int? PartyHostId { get; set; }

        public string? ProductName { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public string? ProductUrl { get; set; }

        public int? ProductType { get; set; }

        public string? ProductStyle { get; set; }

        public double? Price { get; set; }

        public string? ProductStatus { get; set; }

        //public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();

    }
}