using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class PunishmentCategory
    {
        [Key]
        public int PunishmentCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ApplicableSins { get; set; } = string.Empty;

        public int MinimumSeverityLevel { get; set; }

        public int MaximumSeverityLevel { get; set; }

        [MaxLength(500)]
        public string StandardProcedures { get; set; } = string.Empty;

        [MaxLength(200)]
        public string RequiredEquipment { get; set; } = string.Empty;

        public int MinimumTorturerRank { get; set; }

        public bool RequiresSupervisorApproval { get; set; }

        [MaxLength(300)]
        public string SafetyGuidelines { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreationDate { get; set; }

        public int CreatedByDemonId { get; set; }

        public DateTime LastUpdated { get; set; }

        [MaxLength(500)]
        public string UpdateNotes { get; set; } = string.Empty;

        public int UsageFrequency { get; set; }

        [MaxLength(300)]
        public string EthicalGuidelines { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon CreatedBy { get; set; } = null!;
        public virtual ICollection<SeverityLevel> SeverityLevels { get; set; } = new List<SeverityLevel>();
    }
}