using Microsoft.EntityFrameworkCore;
using CultManagement.Models;

namespace CultManagement.Data
{
    public class CultManagementDbContext : DbContext
    {
        public CultManagementDbContext(DbContextOptions<CultManagementDbContext> options) : base(options)
        {
        }

        // Core Cult Structure
        public DbSet<Cult> Cults { get; set; }
        public DbSet<CultLeader> CultLeaders { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<InnerCircle> InnerCircleMembers { get; set; }
        public DbSet<Recruit> Recruits { get; set; }
        public DbSet<CultRank> CultRanks { get; set; }
        public DbSet<CultBranch> CultBranches { get; set; }
        public DbSet<CultCouncil> CultCouncils { get; set; }
        public DbSet<SuccessionPlan> SuccessionPlans { get; set; }
        public DbSet<PowerStructure> PowerStructures { get; set; }

        // Rituals & Ceremonies
        public DbSet<Ritual> Rituals { get; set; }
        public DbSet<Sacrifice> Sacrifices { get; set; }
        public DbSet<BloodOath> BloodOaths { get; set; }
        public DbSet<InitiationRite> InitiationRites { get; set; }
        public DbSet<SecretCeremony> SecretCeremonies { get; set; }
        public DbSet<PropheticVision> PropheticVisions { get; set; }
        public DbSet<ChosenOne> ChosenOnes { get; set; }
        public DbSet<SacredText> SacredTexts { get; set; }
        public DbSet<RitualArtifact> RitualArtifacts { get; set; }
        public DbSet<HolyDay> HolyDays { get; set; }

        // Mind Control & Indoctrination
        public DbSet<BrainwashingSession> BrainwashingSessions { get; set; }
        public DbSet<PropagandaMaterial> PropagandaMaterials { get; set; }
        public DbSet<MindControlTechnique> MindControlTechniques { get; set; }
        public DbSet<Deprogramming> DeprogrammingSessions { get; set; }
        public DbSet<ThoughtReform> ThoughtReforms { get; set; }
        public DbSet<IsolationPeriod> IsolationPeriods { get; set; }
        public DbSet<ConfessionSession> ConfessionSessions { get; set; }
        public DbSet<Punishment> Punishments { get; set; }
        public DbSet<RewardSystem> RewardSystems { get; set; }
        public DbSet<LoyaltyTest> LoyaltyTests { get; set; }

        // Finances & Operations
        public DbSet<MoneyLaundering> MoneyLaunderingOperations { get; set; }
        public DbSet<PyramidScheme> PyramidSchemes { get; set; }
        public DbSet<DonationRecord> DonationRecords { get; set; }
        public DbSet<CultAsset> CultAssets { get; set; }
        public DbSet<FrontCompany> FrontCompanies { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<FinancialAudit> FinancialAudits { get; set; }
        public DbSet<TaxEvasion> TaxEvasions { get; set; }
        public DbSet<PropertyDeed> PropertyDeeds { get; set; }
        public DbSet<InvestmentPortfolio> InvestmentPortfolios { get; set; }

        // Facilities & Security
        public DbSet<CompoundLocation> CompoundLocations { get; set; }
        public DbSet<ArmsCache> ArmsCaches { get; set; }
        public DbSet<SurveillanceSystem> SurveillanceSystems { get; set; }
        public DbSet<SafeHouse> SafeHouses { get; set; }
        public DbSet<UndergroundTemple> UndergroundTemples { get; set; }
        public DbSet<RecruitmentCenter> RecruitmentCenters { get; set; }
        public DbSet<TrainingFacility> TrainingFacilities { get; set; }
        public DbSet<SecretMeetingPlace> SecretMeetingPlaces { get; set; }
        public DbSet<EscapeRoute> EscapeRoutes { get; set; }
        public DbSet<SecurityDetail> SecurityDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints

            // Cult relationships
            modelBuilder.Entity<CultLeader>()
                .HasOne(cl => cl.Cult)
                .WithMany(c => c.Leaders)
                .HasForeignKey(cl => cl.CultId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.Cult)
                .WithMany(c => c.Followers)
                .HasForeignKey(f => f.CultId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.CultRank)
                .WithMany(cr => cr.Followers)
                .HasForeignKey(f => f.CultRankId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CultBranch>()
                .HasOne(cb => cb.Cult)
                .WithMany(c => c.Branches)
                .HasForeignKey(cb => cb.CultId)
                .OnDelete(DeleteBehavior.Cascade);

            // Leadership relationships
            modelBuilder.Entity<InnerCircle>()
                .HasOne(ic => ic.CultLeader)
                .WithMany(cl => cl.InnerCircleMembers)
                .HasForeignKey(ic => ic.CultLeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SuccessionPlan>()
                .HasOne(sp => sp.CurrentLeader)
                .WithMany(cl => cl.SuccessionPlans)
                .HasForeignKey(sp => sp.CurrentLeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SuccessionPlan>()
                .HasOne(sp => sp.Successor)
                .WithMany()
                .HasForeignKey(sp => sp.SuccessorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Ritual relationships
            modelBuilder.Entity<Ritual>()
                .HasOne(r => r.Cult)
                .WithMany(c => c.Rituals)
                .HasForeignKey(r => r.CultId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ritual>()
                .HasOne(r => r.CultLeader)
                .WithMany(cl => cl.LeadRituals)
                .HasForeignKey(r => r.CultLeaderId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Sacrifice>()
                .HasOne(s => s.Ritual)
                .WithMany(r => r.Sacrifices)
                .HasForeignKey(s => s.RitualId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mind control relationships
            modelBuilder.Entity<BrainwashingSession>()
                .HasOne(bs => bs.Follower)
                .WithMany(f => f.BrainwashingSessions)
                .HasForeignKey(bs => bs.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BrainwashingSession>()
                .HasOne(bs => bs.MindControlTechnique)
                .WithMany(mct => mct.BrainwashingSessions)
                .HasForeignKey(bs => bs.MindControlTechniqueId)
                .OnDelete(DeleteBehavior.SetNull);

            // Financial relationships
            modelBuilder.Entity<DonationRecord>()
                .HasOne(dr => dr.Follower)
                .WithMany(f => f.Donations)
                .HasForeignKey(dr => dr.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CultAsset>()
                .HasOne(ca => ca.Cult)
                .WithMany(c => c.Assets)
                .HasForeignKey(ca => ca.CultId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasOne(ba => ba.FrontCompany)
                .WithMany(fc => fc.BankAccounts)
                .HasForeignKey(ba => ba.FrontCompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            // Facility relationships
            modelBuilder.Entity<CompoundLocation>()
                .HasOne(cl => cl.Cult)
                .WithMany(c => c.Compounds)
                .HasForeignKey(cl => cl.CultId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompoundLocation>()
                .HasOne(cl => cl.CultBranch)
                .WithMany(cb => cb.Locations)
                .HasForeignKey(cl => cl.CultBranchId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ArmsCache>()
                .HasOne(ac => ac.CompoundLocation)
                .WithMany(cl => cl.ArmsCaches)
                .HasForeignKey(ac => ac.CompoundLocationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many relationships
            modelBuilder.Entity<Ritual>()
                .HasMany(r => r.RequiredArtifacts)
                .WithMany(ra => ra.UsedInRituals)
                .UsingEntity(j => j.ToTable("RitualArtifactUsage"));

            modelBuilder.Entity<SecretCeremony>()
                .HasMany(sc => sc.Participants)
                .WithMany(ic => ic.SecretCeremonies)
                .UsingEntity(j => j.ToTable("SecretCeremonyParticipants"));

            modelBuilder.Entity<CultCouncil>()
                .HasMany(cc => cc.Members)
                .WithMany(ic => ic.CouncilMemberships)
                .UsingEntity(j => j.ToTable("CultCouncilMembers"));

            modelBuilder.Entity<CultCouncil>()
                .HasMany(cc => cc.MeetingPlaces)
                .WithMany(smp => smp.Councils)
                .UsingEntity(j => j.ToTable("CouncilMeetingPlaces"));

            modelBuilder.Entity<RecruitmentCenter>()
                .HasMany(rc => rc.Recruits)
                .WithMany(r => r.VisitedCenters)
                .UsingEntity(j => j.ToTable("RecruitCenterVisits"));

            // Configure decimal precision
            modelBuilder.Entity<Follower>()
                .Property(f => f.MonthlyContribution)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CultRank>()
                .Property(cr => cr.MinimumContribution)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DonationRecord>()
                .Property(dr => dr.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CultAsset>()
                .Property(ca => ca.EstimatedValue)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MoneyLaundering>()
                .Property(ml => ml.OriginalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MoneyLaundering>()
                .Property(ml => ml.CleanedAmount)
                .HasPrecision(18, 2);

            // Configure indexes for performance
            modelBuilder.Entity<Follower>()
                .HasIndex(f => f.ContactEmail)
                .IsUnique();

            modelBuilder.Entity<CultLeader>()
                .HasIndex(cl => new { cl.FirstName, cl.LastName });

            modelBuilder.Entity<BankAccount>()
                .HasIndex(ba => ba.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<CompoundLocation>()
                .HasIndex(cl => new { cl.Latitude, cl.Longitude });
        }
    }
}