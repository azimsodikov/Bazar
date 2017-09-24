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

namespace Bazar.Controllers
{
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }
        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentTypes.ToListAsync());
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTypes = await _context.PaymentTypes
                .SingleOrDefaultAsync(m => m.PaymentTypesID == id);
            if (paymentTypes == null)
            {
                return NotFound();
            }

            return View(paymentTypes);
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentTypes paymentTypes)
        {
            ModelState.Remove("PaymentTypes.User");
            
                var user = await GetCurrentUserAsync();
                paymentTypes.User = user;
                paymentTypes.IsActive = true;

                _context.Add(paymentTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction("OrderDetails", "Orders");

        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTypes = await _context.PaymentTypes.SingleOrDefaultAsync(m => m.PaymentTypesID == id);
            if (paymentTypes == null)
            {
                return NotFound();
            }
            return View(paymentTypes);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentTypesID,Type,AccountNumber,ExpDate,SecurityCode,IsActive")] PaymentTypes paymentTypes)
        {
            if (id != paymentTypes.PaymentTypesID)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(paymentTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypesExists(paymentTypes.PaymentTypesID))
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

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTypes = await _context.PaymentTypes
                .SingleOrDefaultAsync(m => m.PaymentTypesID == id);
            if (paymentTypes == null)
            {
                return NotFound();
            }

            return View(paymentTypes);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentTypes = await _context.PaymentTypes.SingleOrDefaultAsync(m => m.PaymentTypesID == id);
            _context.PaymentTypes.Remove(paymentTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PaymentTypesExists(int id)
        {
            return _context.PaymentTypes.Any(e => e.PaymentTypesID == id);
        }
    }
}
