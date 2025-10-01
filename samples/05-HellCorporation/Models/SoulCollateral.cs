using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulCollateral
    {
        [Key]
        public int SoulCollateralId { get; set; }

        public int SoulContractId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CollateralType { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanToValueRatio { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Seized, Released, Forfeited

        public DateTime PledgeDate { get; set; }

        public DateTime? SeizureDate { get; set; }

        [MaxLength(500)]
        public string SeizureReason { get; set; } = string.Empty;

        public int CustodianDemonId { get; set; }

        [MaxLength(200)]
        public string StorageLocation { get; set; } = string.Empty;

        public bool RequiresSpecialHandling { get; set; }

        [MaxLength(300)]
        public string SpecialInstructions { get; set; } = string.Empty;

        public DateTime LastAppraisalDate { get; set; }

        public DateTime NextAppraisalDue { get; set; }

        [MaxLength(500)]
        public string AppraisalNotes { get; set; } = string.Empty;

        public bool IsInsured { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InsuranceValue { get; set; }

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon Custodian { get; set; } = null!;
    }
}