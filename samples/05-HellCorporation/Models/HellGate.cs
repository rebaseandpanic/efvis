using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class HellGate
    {
        [Key]
        public int HellGateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string GateName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string GateType { get; set; } = string.Empty; // Entry, Exit, Transfer, Emergency

        public int GuardianDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Closed, Maintenance, Emergency

        public DateTime LastInspectionDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TrafficVolumeDaily { get; set; }

        public int SecurityLevel { get; set; } // 1-10

        [MaxLength(200)]
        public string AccessRequirements { get; set; } = string.Empty;

        public bool RequiresKeycard { get; set; }

        public bool RequiresBiometric { get; set; }

        public bool HasAlarmSystem { get; set; }

        [MaxLength(300)]
        public string SecurityFeatures { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaintenanceCost { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(500)]
        public string OperatingInstructions { get; set; } = string.Empty;

        public bool IsEmergencyExit { get; set; }

        [MaxLength(200)]
        public string ConnectedRealms { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Guardian { get; set; } = null!;
        public virtual ICollection<AccessControl> AccessControls { get; set; } = new List<AccessControl>();
    }
}