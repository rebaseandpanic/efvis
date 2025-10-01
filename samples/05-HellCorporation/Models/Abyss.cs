using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Abyss
    {
        [Key]
        public int AbyssId { get; set; }

        [Required]
        [MaxLength(100)]
        public string AbyssName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Column(TypeName = "decimal(15,2)")]
        public decimal DepthMeters { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DiameterMeters { get; set; }

        [MaxLength(100)]
        public string AbyssType { get; set; } = string.Empty;

        public int GuardianDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Sealed, Monitored, Unstable

        [Column(TypeName = "decimal(10,2)")]
        public decimal DarknessLevel { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TemperatureBottom { get; set; }

        public bool IsInfinite { get; set; }

        [MaxLength(500)]
        public string Contents { get; set; } = string.Empty;

        [MaxLength(200)]
        public string AccessRestrictions { get; set; } = string.Empty;

        public bool RequiresSpecialEquipment { get; set; }

        [MaxLength(300)]
        public string SafetyProtocols { get; set; } = string.Empty;

        public int LastInspectionDepth { get; set; }

        public DateTime LastInspectionDate { get; set; }

        [MaxLength(500)]
        public string InspectionFindings { get; set; } = string.Empty;

        [MaxLength(200)]
        public string KnownHazards { get; set; } = string.Empty;

        [MaxLength(300)]
        public string SupernaturalProperties { get; set; } = string.Empty;

        public bool IsContainmentFacility { get; set; }

        // Navigation properties
        public virtual Demon Guardian { get; set; } = null!;
    }
}