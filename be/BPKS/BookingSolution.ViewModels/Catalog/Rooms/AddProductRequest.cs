using BookingSolution.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Rooms
{
    public class AddProductRequest
    {
        public int? PartyId { get; set; }
        public int? RoomId { get; set; }
        public List<ProductlistRequest>? listproducts { get; set; }
    }
}
