using BasketModuleEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BasketModuleDal.Context
{
    public class BasketModuleContext : DbContext
    {
        public BasketModuleContext()
        {

        }

        public BasketModuleContext(DbContextOptions<BasketModuleContext> options)
            : base(options)
        {
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                string message = "BasketModule On model creating modelBuilder is null";
                throw new ArgumentNullException(message);
            }

            #region Default Data İnsert
            Currency currency = new Currency()
            {
                CurrencyID = 1,
                Code = "TRY",
                Name = "Türk Lirası",
                OperationDate = DateTime.Now,
                OperationType = BasketModuleEntities.GeneralEntities.OperationTypes.Added
            };

            modelBuilder.Entity<Currency>()
               .HasData(currency);

            Product product = new Product
            {
                ProductID = 1,
                Code = "KRMZ10",
                Name = "10 Adet Kırmızı Gül",
                Price = 100,
                CurrencyID = currency.CurrencyID,
                OperationDate = DateTime.Now,
                OperationType = BasketModuleEntities.GeneralEntities.OperationTypes.Added
            };

            modelBuilder.Entity<Product>()
               .HasData(product);
            #endregion
        }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketProduct> BasketProduct { get; set; }

    }
}