using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bazar.Data;
using Bazar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Bazar.Models.ViewModels;

namespace Bazar.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.PaymentTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.PaymentTypes)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypesID"] = new SelectList(_context.Set<PaymentTypes>(), "PaymentTypesID", "Type");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,DateCreated,PaymentTypesID")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PaymentTypesID"] = new SelectList(_context.Set<PaymentTypes>(), "PaymentTypesID", "Type", orders.PaymentTypesID);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["PaymentTypesID"] = new SelectList(_context.Set<PaymentTypes>(), "PaymentTypesID", "Type", orders.PaymentTypesID);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,DateCreated,PaymentTypesID")] Orders orders)
        {
            if (id != orders.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderID))
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
            ViewData["PaymentTypesID"] = new SelectList(_context.Set<PaymentTypes>(), "PaymentTypesID", "Type", orders.PaymentTypesID);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.PaymentTypes)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
        // This method creates new instance of order
        public async Task<Orders> CreateOrder()
        {
            var order = new Orders();
            order.ApplicationUser = await GetCurrentUserAsync();
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        // POST: Orders/BuyProduct/5
        [Authorize]
        public async Task<IActionResult> BuyProduct(int? id)
        {

            var product = await _context.Products.SingleOrDefaultAsync(p => p.ProductID == id);
            if (id == null)
            {
                return NotFound();
            }
            _context.Update(product);

            //This method is expecting qty and product id
            if (id == null)
            {
                return NotFound();
            }
            //once it gets that info it will try to create order
            var user = await GetCurrentUserAsync();
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ApplicationUser.Id == user.Id && m.PaymentTypes == null);
            if (order == null)
            {
                order = await CreateOrder();
            }
            //then it will try to add order to the join table in OrderProduct table

            var orderProduct = new OrderProduct();
            orderProduct.Order = order;
            orderProduct.Product = await _context.Products.SingleOrDefaultAsync(p => p.ProductID == id);
            _context.Add(orderProduct);
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Products", new { id = id });
        }
        // This method creates new instance of order
        [HttpPost]
        public async Task<IActionResult> ShoppCartDelete(int? id, string returnurl)
        {
            
            var orderProducts = await _context.OrderProduct.FirstOrDefaultAsync(m => m.ProductID == id);
            _context.OrderProduct.Remove(orderProducts);
            await _context.SaveChangesAsync();

            
            return Redirect($"http://{returnurl}");
        }
        public async Task<IActionResult> OrderDetails()
        {
            var user = await GetCurrentUserAsync();
            var vm = new ConfirmViewModel(_context, user);
            if(vm.PayMethods.Count() == 0)
            {
                return RedirectToAction("Create", "PaymentTypes");
            }
            else if(vm.Addresses.Count() == 0)
            {
                return RedirectToAction("Create", "Addresses");
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int addID, int payID)
        {
            var user = await GetCurrentUserAsync();
            var currentOrder = await _context.Orders.Include("OrderProduct.Product").SingleOrDefaultAsync(o => o.PaymentTypes == null && o.ApplicationUser.Id == user.Id);

            if (currentOrder == null || currentOrder.OrderProduct == null)
            {
                return NotFound();
            }
            foreach (var item in currentOrder.OrderProduct)
            {
                item.Product.Quantity = item.Product.Quantity - 1;
                _context.Products.Update(item.Product);
            }
            currentOrder.PaymentTypesID = payID;
            currentOrder.AddressID = addID;
            _context.Orders.Update(currentOrder);
            await _context.SaveChangesAsync();
            var orderID = new { orderID = currentOrder.OrderID };
            return RedirectToAction("Index", orderID);
        }
    }
}
