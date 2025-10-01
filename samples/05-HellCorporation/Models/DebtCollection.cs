using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DebtCollection
    {
        [Key]
        public int DebtCollectionId { get; set; }

        public int SoulContractId { get; set; }

        public int CollectorDemonId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountOwed { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountCollected { get; set; }

        public DateTime CollectionStartDate { get; set; }

        public DateTime? CollectionEndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Written Off, In Litigation

        [MaxLength(500)]
        public string CollectionMethods { get; set; } = string.Empty;

        public int ContactAttempts { get; set; }

        public DateTime LastContactDate { get; set; }

        [MaxLength(500)]
        public string ContactNotes { get; set; } = string.Empty;

        [MaxLength(200)]
        public string PaymentPlan { get; set; } = string.Empty;

        public bool LegalActionTaken { get; set; }

        public DateTime? LegalActionDate { get; set; }

        [MaxLength(500)]
        public string LegalActionDetails { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal CollectionFees { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        [MaxLength(500)]
        public string SpecialCircumstances { get; set; } = string.Empty;

        public bool RequiresApproval { get; set; }

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon Collector { get; set; } = null!;
    }
}