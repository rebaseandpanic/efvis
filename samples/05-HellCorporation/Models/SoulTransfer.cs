using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulTransfer
    {
        [Key]
        public int SoulTransferId { get; set; }

        public int SinnedSoulId { get; set; }

        public int FromDemonId { get; set; }

        public int ToDemonId { get; set; }

        public DateTime TransferDate { get; set; }

        [MaxLength(500)]
        public string TransferReason { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferValue { get; set; }

        [MaxLength(50)]
        public string TransferType { get; set; } = string.Empty; // Sale, Trade, Assignment

        public bool RequiredApproval { get; set; }

        public int? ApprovedByDemonId { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string Terms { get; set; } = string.Empty;

        public DateTime? CompletionDate { get; set; }

        [MaxLength(500)]
        public string CompletionNotes { get; set; } = string.Empty;

        public bool RequiresEscrow { get; set; }

        public int? EscrowAccountId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TransferFee { get; set; }

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon FromDemon { get; set; } = null!;
        public virtual Demon ToDemon { get; set; } = null!;
        public virtual Demon? ApprovedBy { get; set; }
        public virtual EscrowAccount? EscrowAccount { get; set; }
    }
}