using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        public int DemonId { get; set; }

        public int FromRankId { get; set; }

        public int ToRankId { get; set; }

        public DateTime PromotionDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryIncrease { get; set; }

        [MaxLength(500)]
        public string Justification { get; set; } = string.Empty;

        public int ApprovedByDemonId { get; set; }

        [MaxLength(200)]
        public string NewResponsibilities { get; set; } = string.Empty;

        public bool IsTemporary { get; set; }

        public DateTime? ReviewDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Approved";

        public DateTime EffectiveDate { get; set; }

        [MaxLength(500)]
        public string Comments { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual DemonRank FromRank { get; set; } = null!;
        public virtual DemonRank ToRank { get; set; } = null!;
        public virtual Demon ApprovedBy { get; set; } = null!;
    }
}