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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionSting = "Server=DESKTOP-UG59IH4\\SQLEXPRESS; Database=DbAssignmentTT;Trusted_Connection=True;Encrypt=False; User Id=sa; Password=admin";
            optionsBuilder.UseSqlServer(connectionSting);
        }

        /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           {
               var connectionSting = "Server=DESKTOP-UG59IH4\\SQLEXPRESS; Database=DbAssignmentTT;Trusted_Connection=True;Encrypt=False; User Id=sa; Password=admin";
               optionsBuilder.UseSqlServer(connectionSting);
           }
        */



    }
    }

    
 