using Microsoft.EntityFrameworkCore;

namespace HellCorporation.Models
{
    public class HellCorporationDbContext : DbContext
    {
        public HellCorporationDbContext(DbContextOptions<HellCorporationDbContext> options) : base(options) { }

        // Demons & Personnel (1-20)
        public DbSet<Demon> Demons { get; set; }
        public DbSet<DemonRank> DemonRanks { get; set; }
        public DbSet<DemonDepartment> DemonDepartments { get; set; }
        public DbSet<FallenAngel> FallenAngels { get; set; }
        public DbSet<Incubus> Incubi { get; set; }
        public DbSet<Succubus> Succubi { get; set; }
        public DbSet<Hellhound> Hellhounds { get; set; }
        public DbSet<ArchDemon> ArchDemons { get; set; }
        public DbSet<LesserDemon> LesserDemons { get; set; }
        public DbSet<DemonContract> DemonContracts { get; set; }
        public DbSet<HellHR> HellHRs { get; set; }
        public DbSet<EmployeeReview> EmployeeReviews { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Demotion> Demotions { get; set; }
        public DbSet<DisciplinaryAction> DisciplinaryActions { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<SickLeave> SickLeaves { get; set; }
        public DbSet<RetirementPlan> RetirementPlans { get; set; }
        public DbSet<DemonUnion> DemonUnions { get; set; }

        // Souls & Contracts (21-40)
        public DbSet<SinnedSoul> SinnedSouls { get; set; }
        public DbSet<SoulContract> SoulContracts { get; set; }
        public DbSet<EternalContract> EternalContracts { get; set; }
        public DbSet<DamnationClause> DamnationClauses { get; set; }
        public DbSet<SoulAcquisition> SoulAcquisitions { get; set; }
        public DbSet<SoulTransfer> SoulTransfers { get; set; }
        public DbSet<SoulValue> SoulValues { get; set; }
        public DbSet<SoulInventory> SoulInventories { get; set; }
        public DbSet<SoulWarehouse> SoulWarehouses { get; set; }
        public DbSet<SoulProcessing> SoulProcessings { get; set; }
        public DbSet<ContractNegotiation> ContractNegotiations { get; set; }
        public DbSet<ContractBreach> ContractBreaches { get; set; }
        public DbSet<ContractRenewal> ContractRenewals { get; set; }
        public DbSet<LegalDepartment> LegalDepartments { get; set; }
        public DbSet<LoopholeExploit> LoopholeExploits { get; set; }
        public DbSet<FinePrint> FineePrints { get; set; }
        public DbSet<SoulCollateral> SoulCollaterals { get; set; }
        public DbSet<DebtCollection> DebtCollections { get; set; }
        public DbSet<SoulForeclosure> SoulForeclosures { get; set; }
        public DbSet<EscrowAccount> EscrowAccounts { get; set; }

        // Torture & Punishment (41-55)
        public DbSet<TortureDepartment> TortureDepartments { get; set; }
        public DbSet<PunishmentMethod> PunishmentMethods { get; set; }
        public DbSet<EternalSuffering> EternalSufferings { get; set; }
        public DbSet<TortureSchedule> TortureSchedules { get; set; }
        public DbSet<PainQuota> PainQuotas { get; set; }
        public DbSet<SufferingMetrics> SufferingMetrics { get; set; }
        public DbSet<TortureEquipment> TortureEquipments { get; set; }
        public DbSet<TortureSession> TortureSessions { get; set; }
        public DbSet<VictimAssignment> VictimAssignments { get; set; }
        public DbSet<TorturerShift> TorturerShifts { get; set; }
        public DbSet<PunishmentCategory> PunishmentCategories { get; set; }
        public DbSet<SeverityLevel> SeverityLevels { get; set; }
        public DbSet<TortureInnovation> TortureInnovations { get; set; }
        public DbSet<QualityControl> QualityControls { get; set; }
        public DbSet<CustomerSatisfaction> CustomerSatisfactions { get; set; }

        // Hell Infrastructure (56-70)
        public DbSet<HellGate> HellGates { get; set; }
        public DbSet<CircleOfHell> CirclesOfHell { get; set; }
        public DbSet<Inferno> Infernos { get; set; }
        public DbSet<Purgatory> Purgatories { get; set; }
        public DbSet<LimboOffice> LimboOffices { get; set; }
        public DbSet<Abyss> Abysses { get; set; }
        public DbSet<FirePit> FirePits { get; set; }
        public DbSet<BrimstoneLake> BrimstoneLakes { get; set; }
        public DbSet<DamnationChamber> DamnationChambers { get; set; }
        public DbSet<EternalFlame> EternalFlames { get; set; }
        public DbSet<HellArchitecture> HellArchitectures { get; set; }
        public DbSet<InfrastructureMaintenance> InfrastructureMaintenances { get; set; }
        public DbSet<TemperatureControl> TemperatureControls { get; set; }
        public DbSet<SecuritySystem> SecuritySystems { get; set; }
        public DbSet<AccessControl> AccessControls { get; set; }

        // Operations & Logistics (71-85)
        public DbSet<SoulHarvesting> SoulHarvestings { get; set; }
        public DbSet<SoulQuota> SoulQuotas { get; set; }
        public DbSet<DailyQuota> DailyQuotas { get; set; }
        public DbSet<MonthlyTarget> MonthlyTargets { get; set; }
        public DbSet<PerformanceMetric> PerformanceMetrics { get; set; }
        public DbSet<ProductivityReport> ProductivityReports { get; set; }
        public DbSet<OperationalEfficiency> OperationalEfficiencies { get; set; }
        public DbSet<WorkflowProcess> WorkflowProcesses { get; set; }
        public DbSet<LogisticsChain> LogisticsChains { get; set; }
        public DbSet<SupplyManagement> SupplyManagements { get; set; }
        public DbSet<InventorySystem> InventorySystems { get; set; }
        public DbSet<AssetTracking> AssetTrackings { get; set; }
        public DbSet<QualityAssurance> QualityAssurances { get; set; }
        public DbSet<ProcessOptimization> ProcessOptimizations { get; set; }
        public DbSet<ContinuousImprovement> ContinuousImprovements { get; set; }

        // Finance & Accounting (86-95)
        public DbSet<HellPayroll> HellPayrolls { get; set; }
        public DbSet<DemonSalary> DemonSalaries { get; set; }
        public DbSet<BonusStructure> BonusStructures { get; set; }
        public DbSet<HellTax> HellTaxes { get; set; }
        public DbSet<FinancialStatement> FinancialStatements { get; set; }
        public DbSet<BudgetAllocation> BudgetAllocations { get; set; }
        public DbSet<ExpenseReport> ExpenseReports { get; set; }
        public DbSet<RevenueStream> RevenueStreams { get; set; }
        public DbSet<ProfitMargin> ProfitMargins { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }

        // Dark Magic & Rituals (96-100)
        public DbSet<DarkMagic> DarkMagics { get; set; }
        public DbSet<DemonicRitual> DemonicRituals { get; set; }
        public DbSet<Necromancy> Necromancies { get; set; }
        public DbSet<CursedObject> CursedObjects { get; set; }
        public DbSet<PossessionCase> PossessionCases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            ConfigureDemonRelationships(modelBuilder);
            ConfigureSoulRelationships(modelBuilder);
            ConfigureTortureRelationships(modelBuilder);
            ConfigureInfrastructureRelationships(modelBuilder);
            ConfigureOperationsRelationships(modelBuilder);
            ConfigureFinanceRelationships(modelBuilder);
            ConfigureMagicRelationships(modelBuilder);
        }

        private void ConfigureDemonRelationships(ModelBuilder modelBuilder)
        {
            // Demon relationships
            modelBuilder.Entity<Demon>()
                .HasOne(d => d.DemonRank)
                .WithMany(r => r.Demons)
                .HasForeignKey(d => d.DemonRankId);

            modelBuilder.Entity<Demon>()
                .HasOne(d => d.DemonDepartment)
                .WithMany(dept => dept.Demons)
                .HasForeignKey(d => d.DemonDepartmentId);

            // FallenAngel one-to-one with Demon
            modelBuilder.Entity<FallenAngel>()
                .HasOne(fa => fa.Demon)
                .WithOne()
                .HasForeignKey<FallenAngel>(fa => fa.DemonId);

            // Multiple foreign key relationships
            modelBuilder.Entity<EmployeeReview>()
                .HasOne(er => er.Demon)
                .WithMany(d => d.EmployeeReviews)
                .HasForeignKey(er => er.DemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeReview>()
                .HasOne(er => er.Reviewer)
                .WithMany()
                .HasForeignKey(er => er.ReviewerDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Self-referencing relationship for LesserDemon supervisor
            modelBuilder.Entity<LesserDemon>()
                .HasOne(ld => ld.Supervisor)
                .WithMany()
                .HasForeignKey(ld => ld.SupervisorDemonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureSoulRelationships(ModelBuilder modelBuilder)
        {
            // Soul contract relationships
            modelBuilder.Entity<SoulContract>()
                .HasOne(sc => sc.SinnedSoul)
                .WithMany(ss => ss.SoulContracts)
                .HasForeignKey(sc => sc.SinnedSoulId);

            modelBuilder.Entity<EternalContract>()
                .HasOne(ec => ec.SoulContract)
                .WithMany(sc => sc.EternalContracts)
                .HasForeignKey(ec => ec.SoulContractId);

            // Prevent cascade deletes on foreign keys to avoid cycles
            modelBuilder.Entity<SoulTransfer>()
                .HasOne(st => st.FromDemon)
                .WithMany()
                .HasForeignKey(st => st.FromDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SoulTransfer>()
                .HasOne(st => st.ToDemon)
                .WithMany()
                .HasForeignKey(st => st.ToDemonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureTortureRelationships(ModelBuilder modelBuilder)
        {
            // Torture session relationships
            modelBuilder.Entity<TortureSession>()
                .HasOne(ts => ts.Torturer)
                .WithMany()
                .HasForeignKey(ts => ts.TorturerDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TortureSession>()
                .HasOne(ts => ts.ReviewedBy)
                .WithMany()
                .HasForeignKey(ts => ts.ReviewedByDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Victim assignment relationships
            modelBuilder.Entity<VictimAssignment>()
                .HasOne(va => va.Torturer)
                .WithMany()
                .HasForeignKey(va => va.TorturerDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VictimAssignment>()
                .HasOne(va => va.AssignedBy)
                .WithMany()
                .HasForeignKey(va => va.AssignedByDemonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureInfrastructureRelationships(ModelBuilder modelBuilder)
        {
            // Circle of Hell relationships
            modelBuilder.Entity<CircleOfHell>()
                .HasOne(coh => coh.Ruler)
                .WithMany()
                .HasForeignKey(coh => coh.RulerDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Access control relationships
            modelBuilder.Entity<AccessControl>()
                .HasOne(ac => ac.GrantedBy)
                .WithMany()
                .HasForeignKey(ac => ac.GrantedByDemonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureOperationsRelationships(ModelBuilder modelBuilder)
        {
            // Soul harvesting relationships
            modelBuilder.Entity<SoulHarvesting>()
                .HasOne(sh => sh.Harvester)
                .WithMany()
                .HasForeignKey(sh => sh.HarvesterDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SoulHarvesting>()
                .HasOne(sh => sh.Supervisor)
                .WithMany()
                .HasForeignKey(sh => sh.SupervisorDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SoulHarvesting>()
                .HasOne(sh => sh.QualityControlBy)
                .WithMany()
                .HasForeignKey(sh => sh.QualityControlByDemonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureFinanceRelationships(ModelBuilder modelBuilder)
        {
            // Finance relationships - straightforward one-to-many
            modelBuilder.Entity<HellPayroll>()
                .HasOne(hp => hp.Demon)
                .WithMany()
                .HasForeignKey(hp => hp.DemonId);

            modelBuilder.Entity<DemonSalary>()
                .HasOne(ds => ds.Demon)
                .WithMany()
                .HasForeignKey(ds => ds.DemonId);
        }

        private void ConfigureMagicRelationships(ModelBuilder modelBuilder)
        {
            // Dark magic relationships
            modelBuilder.Entity<DarkMagic>()
                .HasOne(dm => dm.Caster)
                .WithMany()
                .HasForeignKey(dm => dm.CasterDemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DemonicRitual>()
                .HasOne(dr => dr.Leader)
                .WithMany()
                .HasForeignKey(dr => dr.LeaderDemonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}