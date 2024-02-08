using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace BPKS.Entities
{
    //[Table("Products")]
    public class Product
    {
        public int ProductId { get; set; }
        public int PartyHostId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public string ProductStatus { get; set; }

        //public ICollection<ProductCategory> Categories { get; set; }
    }
}
