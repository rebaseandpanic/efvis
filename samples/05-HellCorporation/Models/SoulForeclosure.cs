using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulForeclosure
    {
        [Key]
        public int SoulForeclosureId { get; set; }

        public int SoulContractId { get; set; }

        public int InitiatedByDemonId { get; set; }

        public DateTime ForeclosureDate { get; set; }

        [MaxLength(1000)]
        public string Reason { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OutstandingDebt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RecoveredValue { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Initiated"; // Initiated, Completed, Challenged, Reversed

        public DateTime? CompletionDate { get; set; }

        [MaxLength(500)]
        public string LegalJustification { get; set; } = string.Empty;

        public bool NoticePeriodRequired { get; set; }

        public int NoticePeriodDays { get; set; }

        public DateTime? NoticeServedDate { get; set; }

        public bool SoulRightOfRedemption { get; set; }

        public DateTime? RedemptionPeriodEnd { get; set; }

        [MaxLength(500)]
        public string AssetSeized { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ForeclosureNotes { get; set; } = string.Empty;

        public bool AppealsAllowed { get; set; }

        public bool AppealFiled { get; set; }

        public DateTime? AppealDate { get; set; }

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon InitiatedBy { get; set; } = null!;
    }
}