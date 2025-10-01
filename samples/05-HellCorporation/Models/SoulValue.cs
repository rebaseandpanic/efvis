using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulValue
    {
        [Key]
        public int SoulValueId { get; set; }

        public int SinnedSoulId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentMarketValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DepreciationRate { get; set; }

        public DateTime LastValuationDate { get; set; }

        public int AppraisedByDemonId { get; set; }

        [MaxLength(500)]
        public string ValuationCriteria { get; set; } = string.Empty;

        [Column(TypeName = "decimal(5,2)")]
        public decimal SinMultiplier { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal RarityBonus { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ConditionFactor { get; set; }

        [MaxLength(200)]
        public string MarketTrends { get; set; } = string.Empty;

        public bool IsAppreciating { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProjectedValue { get; set; }

        public DateTime NextRevaluationDate { get; set; }

        [MaxLength(500)]
        public string ValuationNotes { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Grade { get; set; } = string.Empty; // A+, A, B+, B, C+, C, D

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon AppraisedBy { get; set; } = null!;
    }
}