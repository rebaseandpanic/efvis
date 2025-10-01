using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class ExpenseReport
    {
        [Key]
        public int ExpenseReportId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public int ResponsibleDemonId { get; set; }
        
        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public virtual Demon ResponsibleDemon { get; set; } = null!;
    }
}
