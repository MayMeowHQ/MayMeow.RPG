using MayMeow.RPG.Entities.Identity;
using MayMeow.RPG.Entities.World;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DB Sets for World Entities
        public DbSet<Character> Characters { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ConnectedLocation> ConnectedLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ConnectedLocation>()
               .HasKey(cl => new { cl.ChildId, cl.ParentId });

            builder.Entity<ConnectedLocation>()
                .HasOne(cl => cl.Parent)
                .WithMany(cl => cl.ChildLocations)
                .HasForeignKey(cl => cl.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ConnectedLocation>()
                .HasOne(cl => cl.Child)
                .WithMany(cl => cl.ParrentLocations)
                .HasForeignKey(cl => cl.ChildId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
