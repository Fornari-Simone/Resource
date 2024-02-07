using Microsoft.EntityFrameworkCore;
using Resource.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Repository
{
    public class ResourceDbContext : DbContext
    {
        public ResourceDbContext(DbContextOptions<ResourceDbContext> dbContextOptions) : base(dbContextOptions) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceDb>().HasKey(x => x.ID);

            modelBuilder.Entity<ResourceDb>().HasMany(x => x.resources).WithOne(x => x.resource).HasForeignKey(x => x.Material1);
            modelBuilder.Entity<ResourceDb>().HasMany(x => x.resources).WithOne(x => x.resource).HasForeignKey(x => x.Material2);
            modelBuilder.Entity<ResourceDb>().HasMany(x => x.resources).WithOne(x => x.resource).HasForeignKey(x => x.Material3);

            modelBuilder.Entity<ResourceDb>().Property(x => x.ID).ValueGeneratedOnAdd();
        }
        public DbSet<ResourceDb> ResourceDb { get; set; }
    }
}
