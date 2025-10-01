using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class ContractBreach
    {
        [Key]
        public int ContractBreachId { get; set; }

        public int SoulContractId { get; set; }

        public DateTime BreachDate { get; set; }

        [MaxLength(50)]
        public string BreachType { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string BreachDescription { get; set; } = string.Empty;

        public int SeverityLevel { get; set; } // 1-10

        public int DetectedByDemonId { get; set; }

        public DateTime DetectionDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Under Investigation";

        [Column(TypeName = "decimal(18,2)")]
        public decimal PenaltyAmount { get; set; }

        [MaxLength(500)]
        public string RemedialAction { get; set; } = string.Empty;

        public DateTime? ResolutionDate { get; set; }

        [MaxLength(500)]
        public string ResolutionDetails { get; set; } = string.Empty;

        public bool IsWillful { get; set; }

        public bool RequiresLegalAction { get; set; }

        public int? AssignedLawyerDemonId { get; set; }

        [MaxLength(500)]
        public string Evidence { get; set; } = string.Empty;

        [MaxLength(500)]
        public string SoulDefense { get; set; } = string.Empty;

        public bool AppealFiled { get; set; }

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon DetectedBy { get; set; } = null!;
        public virtual Demon? AssignedLawyer { get; set; }
    }
}