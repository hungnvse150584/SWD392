using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.System.Services
{
    public class ParentOrder
    {
        public Guid parentId {  get; set; }
        public int PartyId { get; set; }
        public int RoomId { get; set; }
        public List<ProductsNQuantity> Items { get; set; }
        public double Total {  get; set; }
    }
    public class ProductsNQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
