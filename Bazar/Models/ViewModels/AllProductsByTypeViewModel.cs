using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazar.Models.ViewModels
{
    public class AllProductsByTypeViewModel
    {
        public ProductTypes ProductType { get; set; }
        public IEnumerable<Products> Products { get; set; }
    }
}
