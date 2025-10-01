using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class PerformanceMetric
    {
        [Key]
        public int PerformanceMetricId { get; set; }
        public int DemonId { get; set; }
        [MaxLength(100)]
        public string MetricName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Target { get; set; }
        public DateTime MeasurementDate { get; set; }
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        public virtual Demon Demon { get; set; } = null!;
    }
}
