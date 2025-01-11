using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> Options) : base(Options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, turkeyZone);
                    entry.Entity.CreatedBy = "";
                    entry.Entity.IsDeleted = false;
                    entry.Entity.IsActive = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.IsDeleted == true)
                    {
                        entry.Entity.DeletedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, turkeyZone);
                        entry.Entity.DeletedBy = "";
                        entry.Entity.IsDeleted = true;
                        entry.Entity.IsActive = false;
                    }
                    else 
                    {
                        entry.Entity.LastModifedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, turkeyZone);
                        entry.Entity.LastModifiedBy = "";
                        entry.Entity.IsDeleted = false;
                        entry.Entity.IsActive = true;
                    }
                    
                    
                }
               
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
