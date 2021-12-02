using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApix.Data.Models;

namespace TaskApix.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        #region Poblic mambers
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Relation
            builder.Entity<Country>()
                .HasMany<Region>(country => country.Regions)
                .WithOne(region => region.Country)
                .HasForeignKey(region => region.CountryId);
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
