using Bazar.Data;
using Bazar.Models;
using Bazar.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazar.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ShoppingCartViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await GetCurrentUserAsync();
            var vm = new OrderDetailsViewModel(_context, user);
            return View(vm);
        }
    }
}
