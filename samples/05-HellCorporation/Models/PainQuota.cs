using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class PainQuota
    {
        [Key]
        public int PainQuotaId { get; set; }

        public int TortureDepartmentId { get; set; }

        public int TorturerDemonId { get; set; }

        public DateTime QuotaPeriodStart { get; set; }

        public DateTime QuotaPeriodEnd { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetPainPoints { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AchievedPainPoints { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CompletionPercentage { get; set; }

        public int TargetSessions { get; set; }

        public int CompletedSessions { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Overdue, Exceeded

        [Column(TypeName = "decimal(18,2)")]
        public decimal BonusEligible { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PenaltyAmount { get; set; }

        [MaxLength(500)]
        public string PerformanceNotes { get; set; } = string.Empty;

        public DateTime LastUpdate { get; set; }

        public int ReviewedByDemonId { get; set; }

        [MaxLength(200)]
        public string QualityMetrics { get; set; } = string.Empty;

        public bool ExceededQuota { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal EfficiencyRating { get; set; }

        [MaxLength(300)]
        public string ImprovementPlan { get; set; } = string.Empty;

        // Navigation properties
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual Demon Torturer { get; set; } = null!;
        public virtual Demon ReviewedBy { get; set; } = null!;
    }
}