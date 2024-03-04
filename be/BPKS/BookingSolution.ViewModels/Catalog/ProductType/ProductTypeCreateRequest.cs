using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingSolution.ViewModels.Catalog.ProductType
{
    public class ProductTypeCreateRequest
    {
        //public int ProductId { get; set; }

        //public int? PartyId { get; set; }

        public string? ProductTypeName { get; set; }

        public string? Status {  get; set; }
        
       // public string? RoomUrl { get; set; }

      //  public string? RoomType { get; set; }

     //   public double? Price { get; set; }
//
     //   public string? Roomstatus { get; set; }

        //public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();
    }
}