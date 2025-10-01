using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SufferingMetrics
    {
        [Key]
        public int SufferingMetricsId { get; set; }

        public int EternalSufferingId { get; set; }

        public DateTime MeasurementDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PainLevel { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AnguishLevel { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DespairLevel { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal HopelessnessLevel { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalSufferingScore { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ResistanceLevel { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal AdaptationRate { get; set; }

        [MaxLength(200)]
        public string BehavioralChanges { get; set; } = string.Empty;

        [MaxLength(200)]
        public string PhysiologicalChanges { get; set; } = string.Empty;

        public bool BreakdownDetected { get; set; }

        [MaxLength(300)]
        public string BreakdownSigns { get; set; } = string.Empty;

        public int MeasuredByDemonId { get; set; }

        [MaxLength(500)]
        public string ObservationNotes { get; set; } = string.Empty;

        public bool RequiresAdjustment { get; set; }

        [MaxLength(300)]
        public string RecommendedAdjustments { get; set; } = string.Empty;

        [MaxLength(20)]
        public string QualityRating { get; set; } = string.Empty; // Excellent, Good, Fair, Poor

        public DateTime NextMeasurementDue { get; set; }

        // Navigation properties
        public virtual EternalSuffering EternalSuffering { get; set; } = null!;
        public virtual Demon MeasuredBy { get; set; } = null!;
    }
}