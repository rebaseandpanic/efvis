using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Inferno
    {
        [Key]
        public int InfernoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string InfernoName { get; set; } = string.Empty;

        public int CircleOfHellId { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Temperature { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal IntensityLevel { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SizeSquareMeters { get; set; }

        public int FuelConsumptionRate { get; set; }

        [MaxLength(100)]
        public string FuelType { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Dormant, Maintenance, Extinguished

        public int MaintenanceStaffCount { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        [MaxLength(500)]
        public string MaintenanceNotes { get; set; } = string.Empty;

        public bool IsControlled { get; set; } = true;

        public int ControlSystemId { get; set; }

        [MaxLength(200)]
        public string SafetyFeatures { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingCost { get; set; }

        public bool IsEternal { get; set; } = true;

        [MaxLength(300)]
        public string PurposeDescription { get; set; } = string.Empty;

        [MaxLength(200)]
        public string EnvironmentalImpact { get; set; } = string.Empty;

        // Navigation properties
        public virtual CircleOfHell CircleOfHell { get; set; } = null!;
        public virtual ICollection<EternalFlame> EternalFlames { get; set; } = new List<EternalFlame>();
    }
}