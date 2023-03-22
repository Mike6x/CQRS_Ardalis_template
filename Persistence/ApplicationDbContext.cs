using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {

                property.SetColumnType("decimal(18,2)");


            }
        }
    }
}
