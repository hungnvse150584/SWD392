namespace BookingSolution.ViewModels.System.Services
{
    public class ProductUserView
    {
        public int ProductId { get; set; }

        // public Guid? PartyHostId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductUrl { get; set; }

        //food or dink
        public int? ProductType { get; set; }

        //phân loại food hoặc dink 

        public string? ProductStyle { get; set; }

        public double? Price { get; set; }

        //public string? ProductStatus { get; set; }

        public string? Description { get; set; }

        //public IFormFile? ThumbnailImage { get; set; }
    }
}
