using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TemperatureControl
    {
        [Key]
        public int TemperatureControlId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SystemName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SystemType { get; set; } = string.Empty; // Heating, Cooling, Climate Control

        public int OperatorDemonId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CurrentTemperature { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TargetTemperature { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumTemperature { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MaximumTemperature { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Operational"; // Operational, Maintenance, Malfunction, Offline

        [Column(TypeName = "decimal(10,2)")]
        public decimal EnergyConsumption { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Efficiency { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(500)]
        public string MaintenanceNotes { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ControlMechanisms { get; set; } = string.Empty;

        public bool HasAutomaticControls { get; set; }

        [MaxLength(200)]
        public string AlarmSystems { get; set; } = string.Empty;

        [MaxLength(300)]
        public string SafetyFeatures { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingCost { get; set; }

        public bool RequiresSpecialFuel { get; set; }

        [MaxLength(100)]
        public string FuelType { get; set; } = string.Empty;

        [MaxLength(500)]
        public string TechnicalSpecifications { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Operator { get; set; } = null!;
    }
}