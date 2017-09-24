using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bazar.Data;
using Microsoft.EntityFrameworkCore;
using Bazar.Models.ViewModels;

namespace Bazar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
           
            var model = new ProductTypeViewModel();

            // Get line items grouped by product id, including count
            var counter = from product in _context.Products
                          group product by product.ProductTypeID into grouped
                          select new { grouped.Key, myCount = grouped.Count() };

            // Build list of Product instances for display in view
            model.GroupedProducts = await (
                from t in _context.ProductTypes
                join p in _context.Products
                on t.ProductTypeID equals p.ProductTypeID
                group new { t, p } by new { t.ProductTypeID, t.CategoryName } into grouped
                select new GroupedProducts
                {
                    TypeId = grouped.Key.ProductTypeID,
                    TypeName = grouped.Key.CategoryName,
                    ProductCount = grouped.Select(x => x.p.ProductID).Count(),
                    Products = grouped.Select(x => x.p).Take(6)
                }).ToListAsync();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
