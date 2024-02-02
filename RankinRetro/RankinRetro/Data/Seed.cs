using Microsoft.AspNetCore.DataProtection;
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

                //Categories
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
                        DiscountName = "HALF",
                        Description = "Enjoy a 50% discount on selected items!",
                        DiscountAmount = new decimal(0.50)
                    },
                    new Discount()
                    {
                        DiscountName = "FLASH",
                        Description = "Limited-time offer: Save 30% on all products in our collection!",
                        DiscountAmount = new decimal(0.30)
                    },
                    new Discount()
                    {
                        DiscountName = "SMMR",
                        Description = "Get ready for summer with 25% off on swimwear and beach accessories.",
                        DiscountAmount = new decimal(0.25)
                    },
                    new Discount()
                    {
                        DiscountName = "BOGOF",
                        Description = "Buy one item and get the second one for free. Don't miss this amazing deal!",
                        DiscountAmount = new decimal(0.00)
                    },
                                });
                    context.SaveChanges();
                }
                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                    new Product()
                    {
                        Name = "Stylish Black Dress",
                        Description = "A classic black dress for various occasions.",
                        Price = new decimal(49.99),
                        BrandId = 1,
                        CategoryId = 1,
                        TypeId = 5,
                        Size = Size.S,
                        Colour = Colour.Black,
                        Material = Material.Cotton,
                        ImageURL = "https://example.com/images/dress1.jpg"
                    },
                    new Product()
                    {
                        Name = "Blue Denim Jeans",
                        Description = "High-quality denim jeans for a comfortable fit.",
                        Price = new decimal(39.99),
                        BrandId = 2,
                        CategoryId = 2,
                        TypeId = 4,
                        Size = Size.M,
                        Colour = Colour.Blue,
                        Material = Material.Denim,
                        ImageURL = "https://example.com/images/jeans1.jpg"
                    },
                    new Product()
                    {
                        Name = "Red Silk Blouse",
                        Description = "Elegant red silk blouse for a luxurious feel.",
                        Price = new decimal(59.99),
                        BrandId = 3,
                        CategoryId = 1,
                        TypeId = 5,
                        Size = Size.L,
                        Colour = Colour.Red,
                        Material = Material.Silk,
                        ImageURL = "https://example.com/images/blouse1.jpg"
                    },
                            new Product()
                    {
                        Name = "Leather Jacket",
                        Description = "Add an edge to your style with a classic leather jacket.",
                        Price = new decimal(99.99),
                        BrandId = 2,
                        CategoryId = 5,
                        TypeId = 7,
                        Size = Size.L,
                        Colour = Colour.Black,
                        Material = Material.Leather,
                        ImageURL = "https://example.com/images/jacket1.jpg"
                    },
                    new Product()
                    {
                        Name = "Striped Sweater",
                        Description = "Stay cozy and stylish in a striped sweater.",
                        Price = new decimal(49.99),
                        BrandId = 3,
                        CategoryId = 2,
                        TypeId = 6,
                        Size = Size.S,
                        Colour = Colour.Blue,
                        Material = Material.Wool,
                        ImageURL = "https://example.com/images/sweater1.jpg"
                    },
                    new Product()
                    {
                        Name = "Floral Print Dress",
                        Description = "Add a touch of femininity with a floral print dress.",
                        Price = new decimal(59.99),
                        BrandId = 1,
                        CategoryId = 1,
                        TypeId = 5,
                        Size = Size.M,
                        Colour = Colour.Pink,
                        Material = Material.Cotton,
                        ImageURL = "https://example.com/images/dress2.jpg"
                    },
                    new Product()
                    {
                        Name = "Classic Black Blazer",
                        Description = "Elevate your look with a classic black blazer.",
                        Price = new decimal(79.99),
                        BrandId = 2,
                        CategoryId = 2,
                        TypeId = 10,
                        Size = Size.L,
                        Colour = Colour.Black,
                        Material = Material.Polyester,
                        ImageURL = "https://example.com/images/blazer1.jpg"
                    },
                    new Product()
                    {
                        Name = "Ripped Skinny Jeans",
                        Description = "Stay on-trend with ripped skinny jeans.",
                        Price = new decimal(49.99),
                        BrandId = 3,
                        CategoryId = 2,
                        TypeId = 4,
                        Size = Size.S,
                        Colour = Colour.Blue,
                        Material = Material.Denim,
                        ImageURL = "https://example.com/images/jeans2.jpg"
                    },
                    new Product()
                    {
                        Name = "Strappy Sandals",
                        Description = "Step out in style with chic strappy sandals.",
                        Price = new decimal(39.99),
                        BrandId = 4,
                        CategoryId = 6,
                        TypeId = 8,
                        Size = Size.M,
                        Colour = Colour.Brown,
                        Material = Material.Leather,
                        ImageURL = "https://example.com/images/sandals1.jpg"
                    },
                    new Product()
                    {
                        Name = "Printed Swim Trunks",
                        Description = "Make a statement at the beach with printed swim trunks.",
                        Price = new decimal(29.99),
                        BrandId = 5,
                        CategoryId = 6,
                        TypeId = 19,
                        Size = Size.L,
                        Colour = Colour.Green,
                        Material = Material.Polyester,
                        ImageURL = "https://example.com/images/swimtrunks1.jpg"
                    },
                    new Product()
                    {
                        Name = "Cozy Pajama Set",
                        Description = "Stay comfortable all night in a cozy pajama set.",
                        Price = new decimal(34.99),
                        BrandId = 1,
                        CategoryId = 7,
                        TypeId = 22,
                        Size = Size.XL,
                        Colour = Colour.Blue,
                        Material = Material.Cotton,
                        ImageURL = "https://example.com/images/pajama1.jpg"
},
                    });
                    context.SaveChanges();
                }
                //Types
                if (!context.Types.Any())
                {
                    context.Types.AddRange(new List<Types>()
                {
                    new Types() 
                    { 
                        Name = "T-Shirt", 
                        Description = "Comfy and casual, perfect for everyday wear." 
                    },
                    new Types() 
                    {   Name = "Blouse", 
                        Description = "Add a touch of elegance to your outfit with a flowy blouse." 
                    },
                    new Types() 
                    {   
                        Name = "Tank Top", 
                        Description = "Stay cool and stylish in a breezy tank top." 
                    },
                    new Types() 
                    { 
                        Name = "Skinny Jeans", 
                        Description = "A timeless classic, skinny jeans hug your curves for a flattering look." 
                    },
                    new Types() 
                    { 
                        Name = "Casual Shift Dress",
                        Description = "Easy and breezy, a shift dress is a go-to for running errands or meeting friends." 
                    },
                    new Types() 
                    { 
                        Name = "Polo Shirt", 
                        Description = "Classic and versatile, a polo shirt is perfect for business casual or weekend outings." 
                    },
                    new Types() 
                    { 
                        Name = "Henley Shirt", 
                        Description = "With its button placket and relaxed fit, a henley shirt is a laid-back alternative to a t-shirt."
                    },
                    new Types() 
                    { 
                        Name = "Hooded Sweatshirt", 
                        Description = "Cozy and comfortable, a hooded sweatshirt is ideal for lounging or staying warm on chilly days." 
                    },
                    new Types() 
                    { 
                        Name = "Chinos", 
                        Description = "Dressy enough for work yet casual enough for weekend wear, chinos are a wardrobe staple." 
                    },
                    new Types() 
                    { 
                        Name = "Cargo Pants",
                        Description = "Functional and stylish, cargo pants offer plenty of pockets for all your essentials." 
                    },
                    new Types() 
                    { 
                        Name = "Athletic Shorts", 
                        Description = "Stay cool and comfortable in lightweight shorts perfect for working out or running errands." 
                    },
                    new Types() 
                    { 
                        Name = "Navy Slim-Fit Suit", 
                        Description = "Make a confident statement in a timeless navy suit with a modern slim fit." 
                    },
                    new Types() 
                    { 
                        Name = "Grey Tweed Suit", 
                        Description = "Add some texture and sophistication to your look with a classic grey tweed suit." 
                    },
                    new Types() 
                    { 
                        Name = "Khaki Chinos Suit", 
                        Description = "Opt for a laid-back yet polished look with a versatile khaki chinos suit." 
                    },
                    new Types() 
                    { 
                        Name = "Graphic T-Shirt", 
                        Description = "Let your child show their personality with a fun and colorful graphic t-shirt." 
                    },
                    new Types() { 
                        Name = "Striped Polo Shirt", 
                        Description = "Classic and cute, a striped polo shirt is perfect for school or playtime." 
                    },
                    new Types() 
                    { 
                        Name = "Hooded Romper", 
                        Description = "Comfy and adorable, a hooded romper is perfect for little ones on the go." 
                    },
                    new Types() 
                    { 
                        Name = "Mesh Tank Top",
                        Description = "Beat the heat in a breathable mesh tank top." 
                    },
                    new Types() 
                    { 
                        Name = "Performance Hoodie", 
                        Description = "Keep warm and dry with a moisture-wicking hoodie perfect for pre - workout or cool - down."
                    },
                    new Types() 
                    { 
                        Name = "High-Waisted Leggings",
                        Description= "Flatter your figure and stay secure with high-waisted leggings." 
                    },
                    new Types() 
                    { 
                        Name = "Running Shorts", 
                        Description = "Move freely and comfortably in lightweight running shorts."
                    }
                });
                    context.SaveChanges();
                }
                //Input azure storage config details
/*                if (!context.Configs.Any())
                {
                    context.Configs.AddRange(new List<AzureStorageConfig>()

                        {
                        new AzureStorageConfig()
                        {
                            Id = "",
                            AccountKey = "",
                            AccountName = "",
                            ImageContainer = "",
                            ThumbnailContainer = ""
                        }
                        });
                    context.SaveChanges();
                }*/
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
                        UserName = adminUserEmail,
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
                    UserName = appUserEmail,
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

