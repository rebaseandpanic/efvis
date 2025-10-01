using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DamnationChamber
    {
        [Key]
        public int DamnationChamberId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ChamberName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ChamberType { get; set; } = string.Empty; // Isolation, Group, Ceremonial, Processing

        [Column(TypeName = "decimal(10,2)")]
        public decimal SizeSquareMeters { get; set; }

        public int MaximumOccupancy { get; set; }

        public int CurrentOccupancy { get; set; }

        public int OperatorDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Available"; // Available, Occupied, Maintenance, Reserved

        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Features { get; set; } = string.Empty;

        public bool HasSoundproofing { get; set; }

        public bool HasTemperatureControl { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TemperatureRange { get; set; }

        [MaxLength(200)]
        public string SecurityMeasures { get; set; } = string.Empty;

        [MaxLength(200)]
        public string MonitoringEquipment { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyOperatingCost { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(500)]
        public string MaintenanceNotes { get; set; } = string.Empty;

        public bool RequiresSpecialClearance { get; set; }

        public int MinimumClearanceLevel { get; set; }

        [MaxLength(300)]
        public string UsageRestrictions { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Operator { get; set; } = null!;
    }
}