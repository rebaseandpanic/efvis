using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SeverityLevel
    {
        [Key]
        public int SeverityLevelId { get; set; }

        public int PunishmentCategoryId { get; set; }

        public int Level { get; set; } // 1-10

        [Required]
        [MaxLength(50)]
        public string LevelName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PainPointsPerMinute { get; set; }

        public int MinimumDurationMinutes { get; set; }

        public int MaximumDurationMinutes { get; set; }

        [MaxLength(500)]
        public string ApprovalRequirements { get; set; } = string.Empty;

        [MaxLength(200)]
        public string RequiredSkills { get; set; } = string.Empty;

        [MaxLength(300)]
        public string SafetyProtocols { get; set; } = string.Empty;

        public bool RequiresMedicalSupervision { get; set; }

        [MaxLength(200)]
        public string MonitoringRequirements { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ExpectedOutcomes { get; set; } = string.Empty;

        public bool IsExperimental { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SuccessRate { get; set; }

        [MaxLength(300)]
        public string SideEffects { get; set; } = string.Empty;

        [MaxLength(200)]
        public string RecoveryRequirements { get; set; } = string.Empty;

        public bool RequiresSpecialFacility { get; set; }

        // Navigation properties
        public virtual PunishmentCategory PunishmentCategory { get; set; } = null!;
    }
}