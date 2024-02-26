namespace BookingSolution.ViewModels.Catalog.Products
{
    public class ProductVm
    {
        public int ProductId { get; set; }

        public int? partyhostid { get; set; }

        public string? productname { get; set; }

        public string? producturl { get; set; }

        public string? producttype { get; set; }

        public string? productstyle { get; set; }

        public double? price { get; set; }

        public string? productstatus { get; set; }

        //public virtual ICollection<ListProduct> ListProducts { get; set; } = new List<ListProduct>();

    }
}