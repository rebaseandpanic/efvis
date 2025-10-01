using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class ContractRenewal
    {
        [Key]
        public int ContractRenewalId { get; set; }

        public int SoulContractId { get; set; }

        public DateTime RenewalDate { get; set; }

        public DateTime NewExpirationDate { get; set; }

        [MaxLength(500)]
        public string UpdatedTerms { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal NewContractValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RenewalFee { get; set; }

        public int ProcessedByDemonId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        [MaxLength(500)]
        public string RenewalJustification { get; set; } = string.Empty;

        public bool IsAutomatic { get; set; }

        public bool RequiredApproval { get; set; }

        public int? ApprovedByDemonId { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [MaxLength(500)]
        public string TermChanges { get; set; } = string.Empty;

        [MaxLength(500)]
        public string AddedClauses { get; set; } = string.Empty;

        [MaxLength(500)]
        public string RemovedClauses { get; set; } = string.Empty;

        public int RenewalPeriodYears { get; set; }

        [MaxLength(300)]
        public string RenewalConditions { get; set; } = string.Empty;

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon ProcessedBy { get; set; } = null!;
        public virtual Demon? ApprovedBy { get; set; }
    }
}