using Domains.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Common
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// connect to a database using default connection string
        /// </summary>
        public AppDbContext()
        {
            _connectionString = "Data Source=domains.sqlite";
        }

        /// <summary>
        /// create a DB context using a custom connection string
        /// </summary>
        /// <param name="connectionString">connectoin string (say, from app config)</param>
        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// create a DB context with custom options
        /// </summary>
        /// <param name="options">options</param>
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Entity> Domains { get; set; }
        public DbSet<DomainProperty> DomainProperties { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
                optionsBuilder.UseSqlite(_connectionString);
            
            //optionsBuilder.EnableSensitiveDataLogging(true);
            
            base.OnConfiguring(optionsBuilder);
        }


        private readonly string _connectionString;
    }
}
