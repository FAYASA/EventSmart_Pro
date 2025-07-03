using EventService.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventService.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configure many-to-many for EventCategory
            builder.Entity<EventCategory>()
                .HasKey(ec => new { ec.EventId, ec.CategoryId });

            builder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventId);

            builder.Entity<EventCategory>()
                .HasOne(ec => ec.Category)
                .WithMany(c => c.EventCategories)
                .HasForeignKey(ec => ec.CategoryId);
        }
    }
}