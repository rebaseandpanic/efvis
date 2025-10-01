using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class EternalSuffering
    {
        [Key]
        public int EternalSufferingId { get; set; }

        public int SinnedSoulId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SufferingType { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; } = true;

        public int IntensityLevel { get; set; } // 1-10

        [Column(TypeName = "decimal(10,2)")]
        public decimal SufferingPoints { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AccumulatedSuffering { get; set; }

        [MaxLength(200)]
        public string PhysicalSymptoms { get; set; } = string.Empty;

        [MaxLength(200)]
        public string PsychologicalSymptoms { get; set; } = string.Empty;

        public bool IsAdaptationOccurring { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal AdaptationRate { get; set; }

        public DateTime LastIntensityIncrease { get; set; }

        public int IncreasesThisEon { get; set; }

        [MaxLength(500)]
        public string AdministratorNotes { get; set; } = string.Empty;

        public bool RequiresMonitoring { get; set; }

        public int MonitoringFrequencyHours { get; set; }

        [MaxLength(200)]
        public string BreakConditions { get; set; } = string.Empty;

        public bool HasBreakOccurred { get; set; }

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual ICollection<SufferingMetrics> SufferingMetrics { get; set; } = new List<SufferingMetrics>();
    }
}