using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Data.SeedingData
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "<Pending>")]
    public class SeedData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static async Task SeedDataContext(StoreContext context, ILoggerFactory loggerFactory)
        {
            var productsString = File.ReadAllText("../Infrastructure/Data/SeedingData/products.json");
            var brandsString = File.ReadAllText("../Infrastructure/Data/SeedingData/brands.json");
            var typesString = File.ReadAllText("../Infrastructure/Data/SeedingData/types.json");


            var products = JsonConvert.DeserializeObject<List<Product>>(productsString);
            var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsString);
            var types = JsonConvert.DeserializeObject<List<ProductType>>(typesString);

            try
            {
                

                if (!context.ProductBrands.Any())
                {
                    var setting = context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductBrands = ON");
                    brands.ForEach(async p => await context.ProductBrands.AddAsync(p));
                    var close = context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductBrands = OFF");
                }

                if (!context.ProductTypes.Any())
                {
                    var setting = context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductBrands = ON");
                    types.ForEach(async p => await context.ProductTypes.AddAsync(p));
                    var close = context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductBrands = OFF");

                }
                if (!context.Products.Any())
                {
                    products.ForEach(async p => await context.Products.AddAsync(p));
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<SeedData>();
                logger.LogError(e.Message);
            }



        }
    }
}
