using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DemonSalary
    {
        [Key]
        public int DemonSalaryId { get; set; }
        public int DemonId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualSalary { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; }
        public DateTime EffectiveDate { get; set; }
        [MaxLength(200)]
        public string SalaryGrade { get; set; } = string.Empty;
        [MaxLength(500)]
        public string AdjustmentReason { get; set; } = string.Empty;

        public virtual Demon Demon { get; set; } = null!;
    }
}
