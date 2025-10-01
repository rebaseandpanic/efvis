using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class EscrowAccount
    {
        [Key]
        public int EscrowAccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        public int EscrowAgentDemonId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [MaxLength(200)]
        public string Purpose { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Closed, Disputed, Frozen

        [MaxLength(500)]
        public string Terms { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ReleaseConditions { get; set; } = string.Empty;

        public int BeneficiaryCount { get; set; }

        [MaxLength(200)]
        public string Beneficiaries { get; set; } = string.Empty;

        public bool RequiresAllPartiesApproval { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AccruedInterest { get; set; }

        [MaxLength(500)]
        public string EscrowInstructions { get; set; } = string.Empty;

        public DateTime LastActivity { get; set; }

        [MaxLength(500)]
        public string ActivityLog { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon EscrowAgent { get; set; } = null!;
        public virtual ICollection<SoulTransfer> SoulTransfers { get; set; } = new List<SoulTransfer>();
    }
}