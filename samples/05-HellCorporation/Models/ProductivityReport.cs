using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class ProductivityReport
    {
        [Key]
        public int ProductivityReportId { get; set; }
        public int DemonDepartmentId { get; set; }
        public DateTime ReportDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ProductivityScore { get; set; }
        public int TotalTasksCompleted { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal EfficiencyRating { get; set; }
        [MaxLength(1000)]
        public string Summary { get; set; } = string.Empty;

        public virtual DemonDepartment DemonDepartment { get; set; } = null!;
    }
}
