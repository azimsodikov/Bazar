using Bazar.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazar.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public List<SelectListItem> ProductTypeID { get; set; }
        public Products Product { get; set; }
        public ProductCreateViewModel(ApplicationDbContext ctx)
        {
            this.ProductTypeID = ctx.ProductTypes
                                    .OrderBy(l => l.CategoryName)
                                    .AsEnumerable()
                                    .Select(li => new SelectListItem
                                    {
                                        Text = li.CategoryName,
                                        Value = li.ProductTypeID.ToString()
                                    }).ToList();
            this.ProductTypeID.Insert(0, new SelectListItem
            {
                Text = "Choose category...",
                Value = "0"
            });


        }
        public ProductCreateViewModel(){}
        public IFormFile Image { get; set; }
    }
}
