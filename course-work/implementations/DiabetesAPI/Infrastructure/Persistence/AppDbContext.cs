using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealEntry> Meals { get; set; }
        public DbSet<FoodEntry> FoodEntries { get; set; }
        public DbSet<GlucoseEntry> GlucoseEntries { get; set; }
        public DbSet<InsulinEntry> InsulinEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetProperties())
                 .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
                // zadava ednakva preciznost na vsichki decimal properties
            }

            modelBuilder.Entity<MealEntry>()
                .HasOne(m => m.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<FoodEntry>()
                .HasOne(fe => fe.MealEntry)
                .WithMany(m => m.FoodEntries)
                .HasForeignKey(fe => fe.MealEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodEntry>()
                .HasOne(fe => fe.Food)
                .WithMany()
                .HasForeignKey(fe => fe.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GlucoseEntry>()
                .HasOne(g => g.User)
                .WithMany(u => u.GlucoseEntries)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsulinEntry>()
                .HasOne(i => i.User)
                .WithMany(u => u.InsulinEntries)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
