using Microsoft.EntityFrameworkCore;
using CursedMuseum.Models;

namespace CursedMuseum.Data;

public class CursedMuseumDbContext : DbContext
{
    public DbSet<CursedObject> CursedObjects { get; set; }
    public DbSet<Curse> Curses { get; set; }
    public DbSet<Victim> Victims { get; set; }
    public DbSet<Artifact> Artifacts { get; set; }
    public DbSet<DarkRitual> DarkRituals { get; set; }
    public DbSet<Witch> Witches { get; set; }
    public DbSet<ProtectionSpell> ProtectionSpells { get; set; }
    public DbSet<MuseumVault> MuseumVaults { get; set; }
    public DbSet<Curator> Curators { get; set; }
    public DbSet<Hex> Hexes { get; set; }
    public DbSet<Talisman> Talismans { get; set; }
    public DbSet<DemonicEntity> DemonicEntities { get; set; }
    public DbSet<AncientText> AncientTexts { get; set; }
    public DbSet<BloodSacrifice> BloodSacrifices { get; set; }
    public DbSet<SealedRoom> SealedRooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=CursedMuseumDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // CursedObject relationships
        modelBuilder.Entity<CursedObject>()
            .HasOne(o => o.Curse)
            .WithMany(c => c.CursedObjects)
            .HasForeignKey(o => o.CurseId);

        modelBuilder.Entity<CursedObject>()
            .HasOne(o => o.Vault)
            .WithMany(v => v.StoredObjects)
            .HasForeignKey(o => o.VaultId)
            .IsRequired(false);

        modelBuilder.Entity<CursedObject>()
            .HasMany(o => o.Victims)
            .WithOne(v => v.CursedObject)
            .HasForeignKey(v => v.CursedObjectId);

        modelBuilder.Entity<CursedObject>()
            .HasMany(o => o.ProtectionSpells)
            .WithOne(p => p.ProtectedObject)
            .HasForeignKey(p => p.CursedObjectId);

        // Curse relationships
        modelBuilder.Entity<Curse>()
            .HasOne(c => c.OriginRitual)
            .WithMany()
            .HasForeignKey(c => c.DarkRitualId)
            .IsRequired(false);

        modelBuilder.Entity<Curse>()
            .HasMany(c => c.Hexes)
            .WithOne(h => h.RelatedCurse)
            .HasForeignKey(h => h.CurseId);

        // Witch relationships
        modelBuilder.Entity<Witch>()
            .HasMany(w => w.PerformedRituals)
            .WithOne(r => r.PerformedBy)
            .HasForeignKey(r => r.WitchId)
            .IsRequired(false);

        modelBuilder.Entity<Witch>()
            .HasMany(w => w.CastSpells)
            .WithOne(s => s.CastBy)
            .HasForeignKey(s => s.WitchId);

        modelBuilder.Entity<Witch>()
            .HasMany(w => w.CastHexes)
            .WithOne(h => h.CastBy)
            .HasForeignKey(h => h.WitchId);

        // Artifact relationships
        modelBuilder.Entity<Artifact>()
            .HasOne(a => a.DescribedIn)
            .WithMany(t => t.DescribedArtifacts)
            .HasForeignKey(a => a.AncientTextId)
            .IsRequired(false);

        modelBuilder.Entity<Artifact>()
            .HasMany(a => a.Talismans)
            .WithOne(t => t.LinkedArtifact)
            .HasForeignKey(t => t.ArtifactId);

        // DarkRitual - Artifact many-to-many
        modelBuilder.Entity<DarkRitual>()
            .HasMany(r => r.RequiredArtifacts)
            .WithMany(a => a.UsedInRituals);

        // DarkRitual relationships
        modelBuilder.Entity<DarkRitual>()
            .HasMany(r => r.SummonedEntities)
            .WithOne(e => e.SummonedBy)
            .HasForeignKey(e => e.DarkRitualId);

        modelBuilder.Entity<DarkRitual>()
            .HasMany(r => r.Sacrifices)
            .WithOne(s => s.Ritual)
            .HasForeignKey(s => s.DarkRitualId);

        // MuseumVault relationships
        modelBuilder.Entity<MuseumVault>()
            .HasOne(v => v.AssignedCurator)
            .WithMany(c => c.ManagedVaults)
            .HasForeignKey(v => v.CuratorId)
            .IsRequired(false);

        modelBuilder.Entity<MuseumVault>()
            .HasMany(v => v.ConnectedRooms)
            .WithOne(r => r.Vault)
            .HasForeignKey(r => r.MuseumVaultId);

        // Curator relationships
        modelBuilder.Entity<Curator>()
            .HasMany(c => c.CataloguedTexts)
            .WithOne(t => t.CataloguedBy)
            .HasForeignKey(t => t.CuratorId)
            .IsRequired(false);

        // DemonicEntity relationships
        modelBuilder.Entity<DemonicEntity>()
            .HasOne(e => e.ContainedIn)
            .WithMany(r => r.ContainedEntities)
            .HasForeignKey(e => e.SealedRoomId)
            .IsRequired(false);

        // BloodSacrifice relationships
        modelBuilder.Entity<BloodSacrifice>()
            .HasOne(s => s.Victim)
            .WithMany(v => v.RelatedSacrifices)
            .HasForeignKey(s => s.VictimId)
            .IsRequired(false);
    }
}