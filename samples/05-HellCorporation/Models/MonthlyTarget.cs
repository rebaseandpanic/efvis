using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class MonthlyTarget
    {
        [Key]
        public int MonthlyTargetId { get; set; }
        public int DemonDepartmentId { get; set; }
        public DateTime TargetMonth { get; set; }
        public int TargetSouls { get; set; }
        public int AchievedSouls { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetRevenue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AchievedRevenue { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public virtual DemonDepartment DemonDepartment { get; set; } = null!;
    }
}
