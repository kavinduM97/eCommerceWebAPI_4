using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebAPI.DataAccess
{
    public class DbbContext : Microsoft.EntityFrameworkCore.DbContext
    {


        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
       
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionSting = "Server=DESKTOP-UG59IH4\\SQLEXPRESS; Database=DbAssignmentjj;Trusted_Connection=True;Encrypt=False; User Id=sa; Password=admin";
            optionsBuilder.UseSqlServer(connectionSting);
        }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
         .HasOne(p => p.Category)
         .WithMany(pc => pc.Products)
         .HasForeignKey(p => p.ProductCategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UEmail);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);



        }  */
    }
}

    
 