using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Purgatory
    {
        [Key]
        public int PurgatoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SectionName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        public int SupervisorDemonId { get; set; }

        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;

        public int CurrentOccupancy { get; set; }

        public int MaximumCapacity { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal UtilizationRate { get; set; }

        [MaxLength(200)]
        public string ProcessingType { get; set; } = string.Empty;

        public int AverageProcessingTimeHours { get; set; }

        [MaxLength(300)]
        public string QualificationCriteria { get; set; } = string.Empty;

        [MaxLength(300)]
        public string ExitCriteria { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingCost { get; set; }

        public int StaffRequirement { get; set; }

        public int CurrentStaffCount { get; set; }

        [MaxLength(200)]
        public string Amenities { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Rules { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Operational";

        public DateTime LastInspectionDate { get; set; }

        [MaxLength(500)]
        public string InspectionNotes { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Supervisor { get; set; } = null!;
    }
}