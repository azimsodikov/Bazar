using Bazar.Data;
using Bazar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bazar.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Orders Order { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        public IEnumerable<Products> Products { get; set; }

        [Display(Name = "Order Count")]
        public int OrderCount { get; set; }

        public Dictionary<Products, int> ProdCount = new Dictionary<Products, int>();

        public string ReturnURL { get; set; }
        public OrderDetailsViewModel(ApplicationDbContext ctx, ApplicationUser user)
        {
            if(user != null)
            {
                var orderID = ctx.Orders.Where(o => o.ApplicationUser.Email == user.Email && o.PaymentTypesID == null).Select(p => p.OrderID).SingleOrDefault();
                if (user != null && orderID != 0)
                {
                    var allProducts = ctx.Products.ToList(); 
                    var productOrders = ctx.OrderProduct.Where(po => po.OrderID == orderID).ToList();

                    Products = (from p in allProducts
                                           join po in productOrders on p.ProductID equals po.ProductID
                                           select p
                                           ).ToList();


                    ProdCount = Products.GroupBy(x => x)
                    .Where(g => g.Count() > 0)
                    .ToDictionary(x => x.Key, y => y.Count());


                    OrderTotal = ctx.OrderProduct.Where(v => v.OrderID == orderID).Sum(p => p.Product.Price);
                    OrderCount = ctx.OrderProduct.Where(c => c.OrderID == orderID).Count();
                }

            }
        }
    }
}
