using Microsoft.EntityFrameworkCore;
using ZombieApocalypse.Models;

namespace ZombieApocalypse.Data
{
    public class ZombieApocalypseDbContext : DbContext
    {
        public ZombieApocalypseDbContext(DbContextOptions<ZombieApocalypseDbContext> options) : base(options)
        {
        }

        // DbSets for all 30 entities
        public DbSet<Zombie> Zombies { get; set; }
        public DbSet<ZombieHorde> ZombieHordes { get; set; }
        public DbSet<Mutation> Mutations { get; set; }
        public DbSet<Infection> Infections { get; set; }
        public DbSet<Survivor> Survivors { get; set; }
        public DbSet<SafeHouse> SafeHouses { get; set; }
        public DbSet<WeaponCache> WeaponCaches { get; set; }
        public DbSet<FoodSupply> FoodSupplies { get; set; }
        public DbSet<MilitaryBase> MilitaryBases { get; set; }
        public DbSet<Helicopter> Helicopters { get; set; }
        public DbSet<RadioTransmission> RadioTransmissions { get; set; }
        public DbSet<Cannibal> Cannibals { get; set; }
        public DbSet<RadiationZone> RadiationZones { get; set; }
        public DbSet<BioweaponLab> BioweaponLabs { get; set; }
        public DbSet<DeadCity> DeadCities { get; set; }
        public DbSet<Bunker> Bunkers { get; set; }
        public DbSet<TrapSystem> TrapSystems { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<QuarantineZone> QuarantineZones { get; set; }
        public DbSet<RaiderGang> RaiderGangs { get; set; }
        public DbSet<AbandonedHospital> AbandonedHospitals { get; set; }
        public DbSet<SupplyDrop> SupplyDrops { get; set; }
        public DbSet<EvacuationPoint> EvacuationPoints { get; set; }
        public DbSet<InfectedAnimal> InfectedAnimals { get; set; }
        public DbSet<LastHuman> LastHumans { get; set; }
        public DbSet<MutantBoss> MutantBosses { get; set; }
        public DbSet<MedicalSupply> MedicalSupplies { get; set; }
        public DbSet<AmmunitionStockpile> AmmunitionStockpiles { get; set; }
        public DbSet<CommunicationTower> CommunicationTowers { get; set; }
        public DbSet<SurvivalGroup> SurvivalGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Zombie entity
            modelBuilder.Entity<Zombie>(entity =>
            {
                entity.HasKey(e => e.ZombieId);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Horde)
                    .WithMany(h => h.Zombies)
                    .HasForeignKey(e => e.HordeId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Mutation)
                    .WithMany(m => m.MutatedZombies)
                    .HasForeignKey(e => e.MutationId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure ZombieHorde entity
            modelBuilder.Entity<ZombieHorde>(entity =>
            {
                entity.HasKey(e => e.HordeId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasMany(e => e.AttackedCities)
                    .WithOne(c => c.AttackingHorde)
                    .HasForeignKey(c => c.AttackingHordeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Mutation entity
            modelBuilder.Entity<Mutation>(entity =>
            {
                entity.HasKey(e => e.MutationId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasOne(e => e.OriginLab)
                    .WithMany(l => l.CreatedMutations)
                    .HasForeignKey(e => e.OriginLabId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Infection entity
            modelBuilder.Entity<Infection>(entity =>
            {
                entity.HasKey(e => e.InfectionId);
                entity.HasOne(e => e.Survivor)
                    .WithMany(s => s.Infections)
                    .HasForeignKey(e => e.SurvivorId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.CauseZombie)
                    .WithMany(z => z.CausedInfections)
                    .HasForeignKey(e => e.CauseZombieId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Survivor entity
            modelBuilder.Entity<Survivor>(entity =>
            {
                entity.HasKey(e => e.SurvivorId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.SafeHouse)
                    .WithMany(s => s.Survivors)
                    .HasForeignKey(e => e.SafeHouseId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.SurvivalGroup)
                    .WithMany(g => g.Members)
                    .HasForeignKey(e => e.SurvivalGroupId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure SafeHouse entity
            modelBuilder.Entity<SafeHouse>(entity =>
            {
                entity.HasKey(e => e.SafeHouseId);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            });

            // Configure WeaponCache entity
            modelBuilder.Entity<WeaponCache>(entity =>
            {
                entity.HasKey(e => e.WeaponCacheId);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.SafeHouse)
                    .WithMany(s => s.WeaponCaches)
                    .HasForeignKey(e => e.SafeHouseId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.MilitaryBase)
                    .WithMany(m => m.WeaponCaches)
                    .HasForeignKey(e => e.MilitaryBaseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure FoodSupply entity
            modelBuilder.Entity<FoodSupply>(entity =>
            {
                entity.HasKey(e => e.FoodSupplyId);
                entity.Property(e => e.FoodType).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.SafeHouse)
                    .WithMany(s => s.FoodSupplies)
                    .HasForeignKey(e => e.SafeHouseId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.SupplyDrop)
                    .WithMany(s => s.FoodSupplies)
                    .HasForeignKey(e => e.SupplyDropId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure MilitaryBase entity
            modelBuilder.Entity<MilitaryBase>(entity =>
            {
                entity.HasKey(e => e.MilitaryBaseId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });

            // Configure Helicopter entity
            modelBuilder.Entity<Helicopter>(entity =>
            {
                entity.HasKey(e => e.HelicopterId);
                entity.Property(e => e.CallSign).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.MilitaryBase)
                    .WithMany(m => m.Helicopters)
                    .HasForeignKey(e => e.MilitaryBaseId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasMany(e => e.ServedEvacuationPoints)
                    .WithMany(p => p.ServicingHelicopters)
                    .UsingEntity("HelicopterEvacuationPoint");
            });

            // Configure RadioTransmission entity
            modelBuilder.Entity<RadioTransmission>(entity =>
            {
                entity.HasKey(e => e.TransmissionId);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.HasOne(e => e.Sender)
                    .WithMany(s => s.SentTransmissions)
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.MilitaryBase)
                    .WithMany(m => m.Transmissions)
                    .HasForeignKey(e => e.MilitaryBaseId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.CommunicationTower)
                    .WithMany(c => c.Transmissions)
                    .HasForeignKey(e => e.CommunicationTowerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Cannibal entity
            modelBuilder.Entity<Cannibal>(entity =>
            {
                entity.HasKey(e => e.CannibalId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.RaiderGang)
                    .WithMany(r => r.Members)
                    .HasForeignKey(e => e.RaiderGangId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure RadiationZone entity
            modelBuilder.Entity<RadiationZone>(entity =>
            {
                entity.HasKey(e => e.RadiationZoneId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.RadiationLevel).HasColumnType("decimal(18,2)");
                entity.Property(e => e.RadiusKilometers).HasColumnType("decimal(18,2)");
            });

            // Configure BioweaponLab entity
            modelBuilder.Entity<BioweaponLab>(entity =>
            {
                entity.HasKey(e => e.BioweaponLabId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.RadiationZone)
                    .WithMany(r => r.BioweaponLabs)
                    .HasForeignKey(e => e.RadiationZoneId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure DeadCity entity
            modelBuilder.Entity<DeadCity>(entity =>
            {
                entity.HasKey(e => e.DeadCityId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Configure Bunker entity
            modelBuilder.Entity<Bunker>(entity =>
            {
                entity.HasKey(e => e.BunkerId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });

            // Configure TrapSystem entity
            modelBuilder.Entity<TrapSystem>(entity =>
            {
                entity.HasKey(e => e.TrapSystemId);
                entity.Property(e => e.TrapType).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.SafeHouse)
                    .WithMany(s => s.TrapSystems)
                    .HasForeignKey(e => e.SafeHouseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Vaccine entity
            modelBuilder.Entity<Vaccine>(entity =>
            {
                entity.HasKey(e => e.VaccineId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.DeveloperLab)
                    .WithMany(l => l.DevelopedVaccines)
                    .HasForeignKey(e => e.DeveloperLabId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.StorageBunker)
                    .WithMany(b => b.StoredVaccines)
                    .HasForeignKey(e => e.StorageBunkerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure QuarantineZone entity
            modelBuilder.Entity<QuarantineZone>(entity =>
            {
                entity.HasKey(e => e.QuarantineZoneId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.DeadCity)
                    .WithMany(d => d.QuarantineZones)
                    .HasForeignKey(e => e.DeadCityId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure RaiderGang entity
            modelBuilder.Entity<RaiderGang>(entity =>
            {
                entity.HasKey(e => e.RaiderGangId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // Configure AbandonedHospital entity
            modelBuilder.Entity<AbandonedHospital>(entity =>
            {
                entity.HasKey(e => e.AbandonedHospitalId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.DeadCity)
                    .WithMany(d => d.AbandonedHospitals)
                    .HasForeignKey(e => e.DeadCityId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure SupplyDrop entity
            modelBuilder.Entity<SupplyDrop>(entity =>
            {
                entity.HasKey(e => e.SupplyDropId);
                entity.HasOne(e => e.MilitaryBase)
                    .WithMany(m => m.SupplyDrops)
                    .HasForeignKey(e => e.MilitaryBaseId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.Helicopter)
                    .WithMany(h => h.SupplyDrops)
                    .HasForeignKey(e => e.HelicopterId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure EvacuationPoint entity
            modelBuilder.Entity<EvacuationPoint>(entity =>
            {
                entity.HasKey(e => e.EvacuationPointId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.DeadCity)
                    .WithMany(d => d.EvacuationPoints)
                    .HasForeignKey(e => e.DeadCityId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure InfectedAnimal entity
            modelBuilder.Entity<InfectedAnimal>(entity =>
            {
                entity.HasKey(e => e.InfectedAnimalId);
                entity.Property(e => e.Species).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Mutation)
                    .WithMany(m => m.MutatedAnimals)
                    .HasForeignKey(e => e.MutationId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure LastHuman entity
            modelBuilder.Entity<LastHuman>(entity =>
            {
                entity.HasKey(e => e.LastHumanId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.Bunker)
                    .WithMany(b => b.LastHumans)
                    .HasForeignKey(e => e.BunkerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure MutantBoss entity
            modelBuilder.Entity<MutantBoss>(entity =>
            {
                entity.HasKey(e => e.MutantBossId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.RadiationZone)
                    .WithMany(r => r.MutantBosses)
                    .HasForeignKey(e => e.RadiationZoneId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure MedicalSupply entity
            modelBuilder.Entity<MedicalSupply>(entity =>
            {
                entity.HasKey(e => e.MedicalSupplyId);
                entity.Property(e => e.SupplyType).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.AbandonedHospital)
                    .WithMany(h => h.MedicalSupplies)
                    .HasForeignKey(e => e.AbandonedHospitalId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.SupplyDrop)
                    .WithMany(s => s.MedicalSupplies)
                    .HasForeignKey(e => e.SupplyDropId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure AmmunitionStockpile entity
            modelBuilder.Entity<AmmunitionStockpile>(entity =>
            {
                entity.HasKey(e => e.AmmunitionStockpileId);
                entity.Property(e => e.AmmoType).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.WeaponCache)
                    .WithMany(w => w.AmmunitionStockpiles)
                    .HasForeignKey(e => e.WeaponCacheId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure CommunicationTower entity
            modelBuilder.Entity<CommunicationTower>(entity =>
            {
                entity.HasKey(e => e.CommunicationTowerId);
                entity.Property(e => e.CallSign).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RangeKilometers).HasColumnType("decimal(18,2)");
            });

            // Configure SurvivalGroup entity
            modelBuilder.Entity<SurvivalGroup>(entity =>
            {
                entity.HasKey(e => e.SurvivalGroupId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}