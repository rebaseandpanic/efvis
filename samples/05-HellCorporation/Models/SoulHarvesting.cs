using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulHarvesting
    {
        [Key]
        public int SoulHarvestingId { get; set; }

        public int HarvesterDemonId { get; set; }

        public int SupervisorDemonId { get; set; }

        public DateTime HarvestDate { get; set; }

        [MaxLength(200)]
        public string HarvestLocation { get; set; } = string.Empty;

        [MaxLength(50)]
        public string HarvestMethod { get; set; } = string.Empty;

        public int SoulsHarvested { get; set; }

        public int TargetQuota { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SuccessRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalValue { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Completed";

        [MaxLength(500)]
        public string HarvestNotes { get; set; } = string.Empty;

        [MaxLength(200)]
        public string EquipmentUsed { get; set; } = string.Empty;

        public int DurationHours { get; set; }

        [MaxLength(300)]
        public string Complications { get; set; } = string.Empty;

        public bool QualityControlPassed { get; set; }

        public int QualityControlByDemonId { get; set; }

        [MaxLength(500)]
        public string QualityNotes { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Harvester { get; set; } = null!;
        public virtual Demon Supervisor { get; set; } = null!;
        public virtual Demon QualityControlBy { get; set; } = null!;
    }
}