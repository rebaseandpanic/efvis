using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class FirePit
    {
        [Key]
        public int FirePitId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PitName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal DiameterMeters { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DepthMeters { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Temperature { get; set; }

        [MaxLength(100)]
        public string FuelSource { get; set; } = string.Empty;

        public int CurrentOccupancy { get; set; }

        public int MaximumCapacity { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Dormant, Maintenance, Full

        public int MaintenanceDemonId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal FuelConsumptionRate { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(200)]
        public string SafetyFeatures { get; set; } = string.Empty;

        [MaxLength(300)]
        public string PurposeDescription { get; set; } = string.Empty;

        public bool HasTemperatureControl { get; set; }

        [MaxLength(200)]
        public string EmissionControl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingCost { get; set; }

        public bool IsEternal { get; set; } = true;

        [MaxLength(300)]
        public string SpecialProperties { get; set; } = string.Empty;

        public int SecurityLevel { get; set; } // 1-10

        // Navigation properties
        public virtual Demon MaintenanceDemon { get; set; } = null!;
    }
}