using Microsoft.AspNetCore.Http;

namespace BookingSolution.ViewModels.Catalog.Products
{
    public class ProductVm
    {
        public int ProductId { get; set; }

        public int? PartyHostId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductUrl { get; set; }

        //food or dink
        public int? ProductType { get; set; }

        //phân loại food hoặc dink 
        
        public string? ProductStyle { get; set; }

        public double? Price { get; set; }

        public string? ProductStatus { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        //public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();
    }
}