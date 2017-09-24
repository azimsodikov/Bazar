using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bazar.Data;
using Bazar.Models;

namespace Bazar.Data
{
    public static class DbInitializer
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var userstore = new UserStore<ApplicationUser>(context);

                if (!context.Roles.Any(r => r.Name == "Administrator"))
                {
                    var role = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
                    await roleStore.CreateAsync(role);
                }

                if (!context.ApplicationUser.Any(u => u.FirstName == "admin"))
                {
                    //  This method will be called after migrating to the latest version.
                    ApplicationUser user = new ApplicationUser
                    {
                        FirstName = "admin",
                        LastName = "admin",
                        UserName = "admin@admin.com",
                        NormalizedUserName = "ADMIN@ADMIN.COM",
                        Email = "admin@admin.com",
                        NormalizedEmail = "ADMIN@ADMIN.COM",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };
                    var passwordHash = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = passwordHash.HashPassword(user, "secret");
                    await userstore.CreateAsync(user);
                    await userstore.AddToRoleAsync(user, "Administrator");
                }
                context.SaveChanges();
                if (context.ProductTypes.Any())
                {
                    // ProductType Table has been seeded
                }
                else
                {
                    context.ProductTypes.AddRange(
                        new ProductTypes
                        {
                            CategoryName = "Shoes"
                        },
                        new ProductTypes
                        {
                            CategoryName = "Bags"
                        },      
                        new ProductTypes
                        {
                            CategoryName = "Wallets"
                        }
                    );
                    context.SaveChanges();
                }

                if (context.Products.Any())
                {
                    // Product table has been seeded
                }
                else
                {
                    var userStore = new UserStore<ApplicationUser>(context);
                    context.Products.AddRange(
                        new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img1.jpg"
                        },
                        new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img2.jpg"
                        },
                        new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img3.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img4.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img5.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img6.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img7.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img8.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img9.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img10.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img11.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img12.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img13.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img14.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img15.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img16.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img17.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img18.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img19.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img20.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img21.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img22.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img23.jpg"
                        },
                        new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img24.jpg"
                        },
                        new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img25.jpg"
                        },
                        new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img26.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img27.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img28.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img29.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img30.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img31.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img32.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img33.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img34.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img35.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img36.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img37.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img38.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img39.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img40.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img41.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img42.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img43.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img44.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img45.jpg"
                        }, new Products
                        {
                            ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Shoes").ProductTypeID,
                            Name = "Leather",
                            Description = "Sneaker with leather",
                            Price = 49,
                            Quantity = 30,
                            Image = "/images/shoes/img46.jpg"
                        },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag1.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag2.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag3.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag4.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag5.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag6.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag7.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag8.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag9.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag10.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag11.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag12.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag13.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag14.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag15.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/shoes/bag16.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag17.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag18.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag19.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Bags").ProductTypeID,
                             Name = "Crocodile Leather",
                             Description = "Faux leather backpack",
                             Price = 89,
                             Quantity = 30,
                             Image = "/images/bags/bag20.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w1.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w2.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w3.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w4.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w5.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w6.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w7.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w8.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w9.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w10.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w11.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w12.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w13.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w14.jpg"
                         },
                         new Products
                         {
                             ProductTypeID = context.ProductTypes.First(p => p.CategoryName == "Wallets").ProductTypeID,
                             Name = "Safiano-effect wallet",
                             Description = "Incorporated coin purse",
                             Price = 25,
                             Quantity = 30,
                             Image = "/images/wallets/w14.jpg"
                         }
                     );
                    context.SaveChanges();
                }
            }
        }
    }
}