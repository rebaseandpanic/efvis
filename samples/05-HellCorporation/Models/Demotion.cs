using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Demotion
    {
        [Key]
        public int DemotionId { get; set; }

        public int DemonId { get; set; }

        public int FromRankId { get; set; }

        public int ToRankId { get; set; }

        public DateTime DemotionDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryReduction { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;

        public int AuthorizedByDemonId { get; set; }

        [MaxLength(200)]
        public string RemovedPrivileges { get; set; } = string.Empty;

        public bool IsTemporary { get; set; }

        public DateTime? ReinstateReviewDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public DateTime EffectiveDate { get; set; }

        [MaxLength(500)]
        public string ImprovementPlan { get; set; } = string.Empty;

        public bool AppealFiled { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual DemonRank FromRank { get; set; } = null!;
        public virtual DemonRank ToRank { get; set; } = null!;
        public virtual Demon AuthorizedBy { get; set; } = null!;
    }
}