using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class EternalFlame
    {
        [Key]
        public int EternalFlameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FlameName { get; set; } = string.Empty;

        public int InfernoId { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Temperature { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal IntensityLevel { get; set; }

        [MaxLength(50)]
        public string FlameColor { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FlameType { get; set; } = string.Empty; // Sacred, Cursed, Punishment, Ceremonial

        public DateTime IgnitionDate { get; set; }

        public bool IsEternal { get; set; } = true;

        [MaxLength(20)]
        public string Status { get; set; } = "Burning"; // Burning, Dormant, Extinguished, Rekindled

        public int KeeperDemonId { get; set; }

        [MaxLength(200)]
        public string FuelSource { get; set; } = string.Empty;

        public bool RequiresMaintenance { get; set; }

        public int MaintenanceIntervalDays { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        [MaxLength(500)]
        public string MaintenanceNotes { get; set; } = string.Empty;

        [MaxLength(300)]
        public string SpecialProperties { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;

        public bool HasSupernaturalEffects { get; set; }

        [MaxLength(300)]
        public string SupernaturalEffects { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaintenanceCost { get; set; }

        public int SecurityLevel { get; set; } // 1-10

        [MaxLength(200)]
        public string AccessRestrictions { get; set; } = string.Empty;

        // Navigation properties
        public virtual Inferno Inferno { get; set; } = null!;
        public virtual Demon Keeper { get; set; } = null!;
    }
}