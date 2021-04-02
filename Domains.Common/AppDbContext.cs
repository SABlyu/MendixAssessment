using Domains.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Common
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entity> Domains { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=domains.sqlite");
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Entity>().Property(e => e.Name);
            modelBuilder.Entity<Entity>().Property(e => e.X);
            modelBuilder.Entity<Entity>().Property(e => e.Y);
        }
    }
}
