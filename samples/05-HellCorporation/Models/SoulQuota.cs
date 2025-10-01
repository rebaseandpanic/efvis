using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulQuota
    {
        [Key]
        public int SoulQuotaId { get; set; }

        public int DemonId { get; set; }

        public int DemonDepartmentId { get; set; }

        public DateTime QuotaPeriodStart { get; set; }

        public DateTime QuotaPeriodEnd { get; set; }

        public int TargetSouls { get; set; }

        public int AchievedSouls { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CompletionPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AchievedValue { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        [Column(TypeName = "decimal(18,2)")]
        public decimal BonusEligible { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PenaltyAmount { get; set; }

        public int ReviewedByDemonId { get; set; }

        [MaxLength(500)]
        public string PerformanceNotes { get; set; } = string.Empty;

        public DateTime LastUpdate { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual DemonDepartment DemonDepartment { get; set; } = null!;
        public virtual Demon ReviewedBy { get; set; } = null!;
    }
}