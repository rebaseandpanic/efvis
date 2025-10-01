using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DemonContract
    {
        [Key]
        public int DemonContractId { get; set; }

        public int DemonId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContractType { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CompensationAmount { get; set; }

        [MaxLength(500)]
        public string Terms { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Conditions { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public bool AutoRenew { get; set; }

        public int? RenewalPeriodDays { get; set; }

        [MaxLength(200)]
        public string Penalties { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Benefits { get; set; } = string.Empty;

        public DateTime LastModified { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}