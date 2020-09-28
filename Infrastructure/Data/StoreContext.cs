using System.Collections.Generic;
using System.IO;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var typesString = File.ReadAllText("../Infrastructure/Data/SeedingData/types.json");
            var brandsString = File.ReadAllText("../Infrastructure/Data/SeedingData/brands.json");
            var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsString);
            var types = JsonConvert.DeserializeObject<List<ProductType>>(typesString);

            modelBuilder.Entity<ProductBrand>().HasData(brands);
            modelBuilder.Entity<ProductType>().HasData(types);

        }
    }
}
