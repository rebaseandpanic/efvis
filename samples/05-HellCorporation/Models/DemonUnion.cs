using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DemonUnion
    {
        [Key]
        public int DemonUnionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string UnionName { get; set; } = string.Empty;

        public int PresidentDemonId { get; set; }

        public int TotalMembers { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal MonthlyDues { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TreasuryBalance { get; set; }

        public DateTime EstablishedDate { get; set; }

        public DateTime LastContractNegotiation { get; set; }

        public DateTime NextContractExpiration { get; set; }

        [MaxLength(500)]
        public string CurrentGrievances { get; set; } = string.Empty;

        public int ActiveStrikes { get; set; }

        [MaxLength(300)]
        public string BargainingDemands { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public bool IsRecognizedByManagement { get; set; } = true;

        [MaxLength(200)]
        public string Jurisdiction { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon President { get; set; } = null!;
    }
}