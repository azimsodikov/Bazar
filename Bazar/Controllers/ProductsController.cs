using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bazar.Models;
using Bazar.Data;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Bazar.Models.ViewModels;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Bazar.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        public ProductsController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _context = ctx;
            _environment = environment;
        }


        // GET: Products
        public async Task<IActionResult> Index(string SearchString)
        {

            if (SearchString != null)
            {
                var products = from p in _context.Products
                               select p;
                if (!String.IsNullOrEmpty(SearchString))
                {
                    products = products.Where(g => g.Name.Contains(SearchString));
                    if(products == null)
                    {
                        return NotFound();
                    }
                    return View(await products.ToListAsync());
                }
            }
            var bazarContext = _context.Products.Include(p => p.ProductTypes);
            return View(await bazarContext.ToListAsync());
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.ProductTypes)
                .SingleOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var viewModel = new ProductCreateViewModel(_context);
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
        {
            if (ModelState.IsValid)

                /*
                    If all other properties validate, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db _context
                */
            if (viewModel.Image != null)
            {
                    if (viewModel.Image.Length > 0)
                    {
                        string directory = Directory.GetCurrentDirectory();
                        string localSavePath = directory + @"\wwwroot\images\" + viewModel.Image.FileName;
                        string dbPath = "/images/" + viewModel.Image.FileName;
                        using (var stream = new FileStream(localSavePath, FileMode.Create))
                        {
                            await viewModel.Image.CopyToAsync(stream);
                        }
                        viewModel.Product.Image = dbPath;
                    }



            
                _context.Add(viewModel.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeID"] = new SelectList(_context.Set<ProductTypes>(), "ProductTypeID", "CategoryName", viewModel.Product.ProductTypeID);
            return View(viewModel);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.SingleOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeID"] = new SelectList(_context.Set<ProductTypes>(), "ProductTypeID", "CategoryName", products.ProductTypeID);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Price,Image,Description,ProductTypeID")] Products products)
        {
            if (id != products.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeID"] = new SelectList(_context.Set<ProductTypes>(), "ProductTypeID", "CategoryName", products.ProductTypeID);
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.ProductTypes)
                .SingleOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.SingleOrDefaultAsync(m => m.ProductID == id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
        // GET: Products/Search
    }
}
