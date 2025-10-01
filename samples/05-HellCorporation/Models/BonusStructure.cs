using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class BonusStructure
    {
        [Key]
        public int BonusStructureId { get; set; }
        public int DemonRankId { get; set; }
        [MaxLength(100)]
        public string BonusType { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseAmount { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal PerformanceMultiplier { get; set; }
        [MaxLength(500)]
        public string QualificationCriteria { get; set; } = string.Empty;

        public virtual DemonRank DemonRank { get; set; } = null!;
    }
}
