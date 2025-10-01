using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulWarehouse
    {
        [Key]
        public int SoulWarehouseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string WarehouseName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        public int ManagerDemonId { get; set; }

        public int TotalCapacity { get; set; }

        public int CurrentOccupancy { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal UtilizationRate { get; set; }

        [MaxLength(100)]
        public string SecurityLevel { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ClimateControl { get; set; } = string.Empty;

        public bool Has24HourSecurity { get; set; }

        public bool HasFireSuppression { get; set; }

        public int SecurityGuardCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingCost { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceScheduled { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Operational";

        public int CircleOfHellId { get; set; }

        // Navigation properties
        public virtual Demon Manager { get; set; } = null!;
        public virtual CircleOfHell CircleOfHell { get; set; } = null!;
        public virtual ICollection<SoulInventory> SoulInventories { get; set; } = new List<SoulInventory>();
        public virtual ICollection<SoulProcessing> SoulProcessings { get; set; } = new List<SoulProcessing>();
    }
}