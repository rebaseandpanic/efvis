using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DailyQuota
    {
        [Key]
        public int DailyQuotaId { get; set; }

        public int DemonId { get; set; }
        public DateTime QuotaDate { get; set; }
        public int TargetSouls { get; set; }
        public int AchievedSouls { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetValue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AchievedValue { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;

        public virtual Demon Demon { get; set; } = null!;
    }
}
