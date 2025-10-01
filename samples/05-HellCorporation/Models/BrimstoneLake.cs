using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class BrimstoneLake
    {
        [Key]
        public int BrimstoneLakeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string LakeName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Column(TypeName = "decimal(15,2)")]
        public decimal SurfaceAreaSquareKilometers { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MaxDepthMeters { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageDepthMeters { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Temperature { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SulfurConcentration { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ToxicityLevel { get; set; }

        public int LakekeeperDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Frozen, Evaporating, Contaminated

        [MaxLength(200)]
        public string PrimaryUse { get; set; } = string.Empty;

        public bool IsNavigable { get; set; }

        [MaxLength(200)]
        public string NavigationHazards { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Wildlife { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaintenanceCost { get; set; }

        public DateTime LastQualityTest { get; set; }

        [MaxLength(500)]
        public string QualityTestResults { get; set; } = string.Empty;

        [MaxLength(200)]
        public string EmissionLevels { get; set; } = string.Empty;

        [MaxLength(300)]
        public string EnvironmentalImpact { get; set; } = string.Empty;

        public bool RequiresFiltering { get; set; }

        [MaxLength(200)]
        public string FilteringSystem { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Lakekeeper { get; set; } = null!;
    }
}