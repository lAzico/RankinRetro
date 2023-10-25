using Microsoft.AspNetCore.Identity;
using RankinRetro.Data;
using RankinRetro.Data.Enum;
using RankinRetro.Models;
using System.Diagnostics;
using System.Net;

namespace RunGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Women's Clothing",
                            Description = "Discover the essence of style and elegance with our exquisite collection of women's clothing. Elevate your wardrobe with a range of fashion-forward pieces, designed to accentuate your unique personality and enhance your everyday look. " +
                            "From chic dresses that make a statement to comfortable casual wear for a relaxed day out, our selection offers diversity for every occasion. " +
                            "Explore the latest trends in women's fashion, whether you're seeking timeless classics or modern, cutting-edge designs. With a variety of sizes, colors, and styles to choose from, our women's clothing category is your destination for unparalleled quality, comfort, and confidence in your personal style. " +
                            "Shop with us and embrace your inner fashionista with pieces that are as exceptional and individual as you are.",
                         },
                         new Category()
                        {
                            Name = "Men's Clothing",
                            Description = "Dress with confidence in our curated collection of men's clothing. " +
                            "Whether you're looking for sophisticated suits for formal occasions or casual attire for everyday comfort, we've got you covered. " +
                            "Discover a wide range of styles, from classic to contemporary, that will elevate your fashion game. With options in various sizes, materials, and colors, our men's clothing category offers the perfect blend of comfort and style. Explore the latest trends, stay on top of your game, and define your unique look with us."
                        },
                        new Category()
                        {
                            Name = "Children's Clothing",
                            Description = "Let your little ones shine with our adorable children's clothing range. " +
                            "From cute and comfy babywear to trendy outfits for kids of all ages, our collection is designed to make your child's style stand out. " +
                            "Dress them in vibrant colors and playful designs that keep them looking stylish and feeling comfortable. " +
                            "Find the perfect fit for every season and occasion with our children's clothing category."
                        },
                        new Category()
                        {
                            Name = "Activewear",
                            Description = "Elevate your active lifestyle with our collection of high-performance activewear. " +
                            "Designed for those who value both style and functionality, our activewear range includes workout apparel, athleisure wear, and accessories. " +
                            "Stay comfortable and confident while achieving your fitness goals."
                        },
                        new Category()
                        {
                            Name = "Outerwear",
                            Description = "Conquer the elements in style with our versatile outerwear collection. " +
                            "From warm and cozy winter coats to lightweight jackets for milder weather, our outerwear category offers a wide range of options for staying comfortable and fashionable in any season."
                        },
                        new Category()
                        {
                            Name = "Swimwear",
                            Description = "Dive into our swimwear selection and make a splash this summer. " +
                            "Find the perfect swimsuits, bikinis, and swim trunks to enjoy the sun, sand, and water in style. " +
                            "Explore a variety of colors, patterns, and sizes for a fantastic beach or pool experience."
                        },
                        new Category()
                        {
                            Name = "Lingerie and Sleepwear",
                            Description = "Feel confident and comfortable in our lingerie and sleepwear collection. " +
                            "From seductive lace to cozy pajamas, we've got the perfect pieces to add a touch of elegance to your intimate moments and ensure a good night's sleep."
                        },
                        new Category()
                        {
                            Name = "Accessories",
                            Description = "Complete your look with our stunning accessories. " +
                           "From handbags to hats, scarves to sunglasses, our accessory category offers the finishing touches that elevate your outfit and reflect your unique style."
                        },

                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Brands.Any())
                {
                    context.Brands.AddRange(new List<Brand>()
                    {
                        new Brand()
                        {
                            Name = "Nike",
                            Description = "One of the world's leading athletic brands, known for its innovative sports and lifestyle products.",
                            Website = "https://www.nike.com/"
                        },
                        new Brand()
                        {
                            Name = "Adidas",
                            Description = "A global sportswear brand offering a wide range of athletic and casual products.",
                            Website = "https://www.adidas.com/"
                        },
                        new Brand()
                        {
                            Name = "Puma",
                            Description = "Puma is known for its sportswear and athletic footwear, offering stylish and comfortable options for sports and daily wear.",
                            Website = "https://www.puma.com/"
                        },
                        new Brand()
                        {
                            Name = "Under Armour",
                            Description = "A performance apparel and footwear brand, specializing in products for athletes and fitness enthusiasts.",
                            Website = "https://www.underarmour.com/"
                        },
                        new Brand()
                        {
                            Name = "New Balance",
                            Description = "A brand known for its high-quality athletic footwear and activewear, providing both style and comfort.",
                            Website = "https://www.newbalance.com/"
                        },

                    });
                    context.SaveChanges();
                }
                if (!context.Discounts.Any())
                {
                    context.Discounts.AddRange(new List<Discount>()
                    {
                    new Discount()
                    {
                        DiscountName = "Half Price",
                        Description = "Enjoy a 50% discount on selected items!",
                        DiscountAmount = 0.50f
                    },
                    new Discount()
                    {
                        DiscountName = "Flash Sale",
                        Description = "Limited-time offer: Save 30% on all products in our collection!",
                        DiscountAmount = 0.30f
                    },
                    new Discount()
                    {
                        DiscountName = "Summer Clearance",
                        Description = "Get ready for summer with 25% off on swimwear and beach accessories.",
                        DiscountAmount = 0.25f
                    },
                    new Discount()
                    {
                        DiscountName = "Buy One, Get One Free",
                        Description = "Buy one item and get the second one for free. Don't miss this amazing deal!",
                        DiscountAmount = 0.00f
                    },
                                });
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                    new Product()
                    {
                        Name = "Stylish Black Dress",
                        Description = "A classic black dress for various occasions.",
                        Price = 49.99f,
                        BrandId = 1,
                        CategoryId = 1,
                        Size = Size.S,
                        Colour = Colour.Black,
                        Material = Material.Cotton,
                        ImageURL = "https://example.com/images/dress1.jpg"
                    },
                    new Product()
                    {
                        Name = "Blue Denim Jeans",
                        Description = "High-quality denim jeans for a comfortable fit.",
                        Price = 39.99f,
                        BrandId = 2,
                        CategoryId = 2,
                        Size = Size.M,
                        Colour = Colour.Blue,
                        Material = Material.Denim,
                        ImageURL = "https://example.com/images/jeans1.jpg"
                    },
                    new Product()
                    {
                        Name = "Red Silk Blouse",
                        Description = "Elegant red silk blouse for a luxurious feel.",
                        Price = 59.99f,
                        BrandId = 3,
                        CategoryId = 1,
                        Size = Size.L,
                        Colour = Colour.Red,
                        Material = Material.Silk,
                        ImageURL = "https://example.com/images/blouse1.jpg"
                    },
                    });
                    context.SaveChanges();

                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
                string adminUserEmail = "earlierbaton937@outlook.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Customer()
                    {
                   
                        FirstName = "Chris",
                        Surname = "Coolguy",
                        UserName = "ChrisCoolguy",
                        Email = adminUserEmail,
                        Title = UserTitle.Mr,
                        AddressFirstline = "15 Waterloo road",
                        AddressSecondline = "",
                        CityTown = "Auckland",
                        AddressPostcode = "BT42 0FG",
                        Gender = Gender.Male,
                        Phone = "07743365343"
                    };

                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "jon.coolguy@gmail.com";

                var newAppUser = new Customer()
                {
                    FirstName = "Jonathan",
                    Surname = "Coolguy",
                    UserName = "JonathanCoolguy",
                    Email = appUserEmail,
                    Title = UserTitle.Mr,
                    AddressFirstline = "10 Waterloo road",
                    AddressSecondline = "",
                    CityTown = "Christchurch",
                    AddressPostcode = "BT35 0JG",
                    Gender = Gender.Other,
                    Phone = "07743985320"
                };

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }

