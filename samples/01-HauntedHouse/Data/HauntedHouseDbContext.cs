using Microsoft.EntityFrameworkCore;
using HauntedHouse.Models;

namespace HauntedHouse.Data;

public class HauntedHouseDbContext : DbContext
{
    public DbSet<Models.HauntedHouse> HauntedHouses { get; set; }
    public DbSet<Ghost> Ghosts { get; set; }
    public DbSet<ParanormalActivity> ParanormalActivities { get; set; }
    public DbSet<Exorcist> Exorcists { get; set; }
    public DbSet<Investigation> Investigations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=HauntedHouseDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.HauntedHouse>()
            .HasMany(h => h.Ghosts)
            .WithOne(g => g.HauntedHouse)
            .HasForeignKey(g => g.HauntedHouseId);

        modelBuilder.Entity<Models.HauntedHouse>()
            .HasMany(h => h.Activities)
            .WithOne(a => a.HauntedHouse)
            .HasForeignKey(a => a.HauntedHouseId);

        modelBuilder.Entity<Models.HauntedHouse>()
            .HasMany(h => h.Investigations)
            .WithOne(i => i.HauntedHouse)
            .HasForeignKey(i => i.HauntedHouseId);

        modelBuilder.Entity<Ghost>()
            .HasMany(g => g.Activities)
            .WithOne(a => a.Ghost)
            .HasForeignKey(a => a.GhostId)
            .IsRequired(false);

        modelBuilder.Entity<Exorcist>()
            .HasMany(e => e.Investigations)
            .WithOne(i => i.Exorcist)
            .HasForeignKey(i => i.ExorcistId);

        modelBuilder.Entity<Investigation>()
            .HasMany(i => i.ActivitiesFound)
            .WithOne(a => a.Investigation)
            .HasForeignKey(a => a.InvestigationId)
            .IsRequired(false);
    }
}