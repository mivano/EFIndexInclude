using System.Collections.Generic;
using EFIndexInclude.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFIndexInclude
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        
            optionsBuilder.ReplaceService<IMigrationsAnnotationProvider, ExtendedSqlServerMigrationsAnnotationProvider>();
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, ExtendedSqlServerMigrationsSqlGenerator>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
            // Add index
            modelBuilder.Entity<Employee>()
                .HasIndex(rrs => new { rrs.FirstName });

            // Add index with include
            modelBuilder.Entity<Employee>()
                .HasIndex(rrs => rrs.LastName)
                .Include<Employee>(rrs => new
                {
                    rrs.Address,
                    rrs.City,
                    rrs.Country
                }) 
                .HasName("IX_IncludeEmployee");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}