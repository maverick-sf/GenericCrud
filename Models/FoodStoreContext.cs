using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Final_assignment.Models
{
    public class FoodStoreContext:IdentityDbContext<IdentityUser>
    {
        public FoodStoreContext() : base("DefaultConnection")
        {
          //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<FoodStoreContext, Final_assignment.Migrations.Configuration>());

        }


        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Product>().HasRequired(p => p.Category)
            //   .WithMany(b => b.Products).HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}