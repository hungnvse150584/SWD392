using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSolution.ViewModels.Catalog.Products
{
    public class ProductDetailViewModel
    {
        public ProductVm ProductDetail { get; set; }
        public List<ProductVm> RelatedProducts { get; set; }
    }
}
