using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulContract
    {
        [Key]
        public int SoulContractId { get; set; }

        public int SinnedSoulId { get; set; }

        public int ContractorDemonId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContractNumber { get; set; } = string.Empty;

        public DateTime SignatureDate { get; set; }

        public DateTime ActivationDate { get; set; }

        [MaxLength(50)]
        public string ContractType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractValue { get; set; }

        [MaxLength(1000)]
        public string Terms { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Obligations { get; set; } = string.Empty;

        public bool IsEternal { get; set; } = true;

        public DateTime? ExpirationDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public bool BreachDetected { get; set; }

        [MaxLength(500)]
        public string BreachDetails { get; set; } = string.Empty;

        public DateTime LastReviewDate { get; set; }

        public bool RenewalEligible { get; set; }

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon ContractorDemon { get; set; } = null!;
        public virtual ICollection<EternalContract> EternalContracts { get; set; } = new List<EternalContract>();
        public virtual ICollection<ContractBreach> ContractBreaches { get; set; } = new List<ContractBreach>();
    }
}