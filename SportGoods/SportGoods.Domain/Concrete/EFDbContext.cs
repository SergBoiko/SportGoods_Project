using Microsoft.AspNet.Identity.EntityFramework;
using SportGoods.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGoods.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public EFDbContext(): base("EFDbContext1")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Product)
                .WithMany(p => p.Orders);

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(p => p.Orders);
        }
    }
}
