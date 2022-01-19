﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Scraping.Entities;

namespace StockData.Scraping.Contexts
{
    public class ScrapingDbContext : Context, IScrapingDbContext
    {
        public ScrapingDbContext(string connectionString, string migrationAssembly)
            : base(connectionString, migrationAssembly)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(s => s.StockPrices)
                .WithOne(c => c.Company)
                .HasForeignKey(f => f.CompanyId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
    }
}
