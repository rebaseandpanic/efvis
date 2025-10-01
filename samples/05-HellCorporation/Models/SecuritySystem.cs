using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SecuritySystem
    {
        [Key]
        public int SecuritySystemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SystemName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SystemType { get; set; } = string.Empty; // Surveillance, Alarm, Access Control, Perimeter

        public int SecurityChiefDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Inactive, Maintenance, Malfunction

        public int SecurityLevel { get; set; } // 1-10

        [MaxLength(200)]
        public string Components { get; set; } = string.Empty;

        public int CameraCount { get; set; }

        public int SensorCount { get; set; }

        public int AlarmPointCount { get; set; }

        [MaxLength(200)]
        public string MonitoringCapabilities { get; set; } = string.Empty;

        public bool Has24HourMonitoring { get; set; }

        [MaxLength(200)]
        public string ResponseProtocols { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingCost { get; set; }

        public DateTime LastSystemCheck { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(500)]
        public string MaintenanceNotes { get; set; } = string.Empty;

        public int AlertsThisMonth { get; set; }

        public int FalseAlarmsThisMonth { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SystemReliability { get; set; }

        [MaxLength(300)]
        public string IntegrationSystems { get; set; } = string.Empty;

        public bool RequiresSpecialClearance { get; set; }

        [MaxLength(200)]
        public string AuthorizedPersonnel { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon SecurityChief { get; set; } = null!;
        public virtual ICollection<AccessControl> AccessControls { get; set; } = new List<AccessControl>();
    }
}