using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class HellPayroll
    {
        [Key]
        public int HellPayrollId { get; set; }
        public int DemonId { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseSalary { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Overtime { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonuses { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPay { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Processed";

        public virtual Demon Demon { get; set; } = null!;
    }
}
