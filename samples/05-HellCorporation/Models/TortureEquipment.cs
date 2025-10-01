using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TortureEquipment
    {
        [Key]
        public int TortureEquipmentId { get; set; }

        public int TortureDepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EquipmentName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string EquipmentType { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Model { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SerialNumber { get; set; } = string.Empty;

        public DateTime PurchaseDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Operational"; // Operational, Maintenance, Broken, Retired

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(500)]
        public string MaintenanceNotes { get; set; } = string.Empty;

        public int UsageHours { get; set; }

        public int EffectivenessRating { get; set; } // 1-10

        [MaxLength(200)]
        public string SafetyFeatures { get; set; } = string.Empty;

        public bool RequiresTraining { get; set; }

        [MaxLength(300)]
        public string OperatingInstructions { get; set; } = string.Empty;

        public bool IsPortable { get; set; }

        [MaxLength(100)]
        public string CurrentLocation { get; set; } = string.Empty;

        // Navigation properties
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual ICollection<TortureSession> TortureSessions { get; set; } = new List<TortureSession>();
    }
}