using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class PunishmentMethod
    {
        [Key]
        public int PunishmentMethodId { get; set; }

        [Required]
        [MaxLength(100)]
        public string MethodName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public int SeverityLevel { get; set; } // 1-10

        public int DurationMinutes { get; set; }

        [MaxLength(200)]
        public string RequiredEquipment { get; set; } = string.Empty;

        [MaxLength(200)]
        public string RequiredSkills { get; set; } = string.Empty;

        public int MinimumTorturerRank { get; set; }

        [MaxLength(500)]
        public string SafetyPrecautions { get; set; } = string.Empty;

        public bool RequiresMedicalSupervision { get; set; }

        [MaxLength(200)]
        public string TargetSinTypes { get; set; } = string.Empty;

        public int EffectivenessRating { get; set; } // 1-10

        [MaxLength(500)]
        public string SideEffects { get; set; } = string.Empty;

        public bool IsExperimental { get; set; }

        public DateTime CreationDate { get; set; }

        public int CreatedByDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public bool RequiresApproval { get; set; }

        // Navigation properties
        public virtual Demon CreatedBy { get; set; } = null!;
        public virtual ICollection<TortureSession> TortureSessions { get; set; } = new List<TortureSession>();
    }
}