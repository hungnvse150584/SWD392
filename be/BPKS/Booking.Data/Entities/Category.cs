using System;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace BPKS.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int PartyId { get; set; }
        public int ImageId { get; set; }
        public string CategoryStatus { get; set; }

        // Foreign key relationships
        public Party Party { get; set; }
        public Image Image { get; set; }

        //public ICollection<ProductCategory> Products { get; set; }

    }
}
