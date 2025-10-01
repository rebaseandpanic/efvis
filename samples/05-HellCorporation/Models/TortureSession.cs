using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TortureSession
    {
        [Key]
        public int TortureSessionId { get; set; }

        public int TortureDepartmentId { get; set; }

        public int SinnedSoulId { get; set; }

        public int TorturerDemonId { get; set; }

        public int PunishmentMethodId { get; set; }

        public int? TortureEquipmentId { get; set; }

        public DateTime SessionDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int DurationMinutes { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PainPointsGenerated { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal EffectivenessRating { get; set; }

        [MaxLength(1000)]
        public string SessionNotes { get; set; } = string.Empty;

        [MaxLength(200)]
        public string SoulReaction { get; set; } = string.Empty;

        public bool BreakdownAchieved { get; set; }

        public bool ComplianceImproved { get; set; }

        [MaxLength(500)]
        public string Complications { get; set; } = string.Empty;

        public bool RequiredMedicalAttention { get; set; }

        [MaxLength(300)]
        public string MedicalNotes { get; set; } = string.Empty;

        [MaxLength(20)]
        public string QualityRating { get; set; } = "Satisfactory";

        public int ReviewedByDemonId { get; set; }

        [MaxLength(500)]
        public string ReviewerComments { get; set; } = string.Empty;

        public bool MetQuotaContribution { get; set; }

        // Navigation properties
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon Torturer { get; set; } = null!;
        public virtual PunishmentMethod PunishmentMethod { get; set; } = null!;
        public virtual TortureEquipment? TortureEquipment { get; set; }
        public virtual Demon ReviewedBy { get; set; } = null!;
    }
}