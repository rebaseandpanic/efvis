using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class FinePrint
    {
        [Key]
        public int FinePrintId { get; set; }

        public int SoulContractId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SectionNumber { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Text { get; set; } = string.Empty;

        public int FontSize { get; set; } = 4;

        [MaxLength(50)]
        public string TextColor { get; set; } = "Gray";

        public bool IsHidden { get; set; }

        public bool IsDeceptive { get; set; }

        [MaxLength(500)]
        public string Purpose { get; set; } = string.Empty;

        public int SeverityLevel { get; set; } // 1-10

        [MaxLength(300)]
        public string LegalConsequences { get; set; } = string.Empty;

        public bool HasBeenChallenged { get; set; }

        public int TimesInvoked { get; set; }

        [MaxLength(500)]
        public string RelatedClauses { get; set; } = string.Empty;

        public bool RequiresSpecialNotice { get; set; }

        [MaxLength(300)]
        public string NoticeRequirements { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public int CreatedByDemonId { get; set; }

        public DateTime LastUpdated { get; set; }

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon CreatedBy { get; set; } = null!;
    }
}