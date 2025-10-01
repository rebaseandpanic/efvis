using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class TortureInnovation
    {
        [Key]
        public int TortureInnovationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string InnovationName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public int InnovatorDemonId { get; set; }

        public DateTime CreationDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Under Development"; // Under Development, Testing, Approved, Rejected, Implemented

        [Column(TypeName = "decimal(10,2)")]
        public decimal EstimatedEfficiencyGain { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DevelopmentCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ImplementationCost { get; set; }

        [MaxLength(500)]
        public string RequiredResources { get; set; } = string.Empty;

        public DateTime? TestingStartDate { get; set; }

        public DateTime? TestingEndDate { get; set; }

        [MaxLength(1000)]
        public string TestResults { get; set; } = string.Empty;

        public bool PassedSafetyReview { get; set; }

        public bool PassedEthicsReview { get; set; }

        public int? ReviewedByDemonId { get; set; }

        public DateTime? ReviewDate { get; set; }

        [MaxLength(500)]
        public string ReviewComments { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Advantages { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Disadvantages { get; set; } = string.Empty;

        public bool RequiresPatent { get; set; }

        [MaxLength(100)]
        public string PatentNumber { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Innovator { get; set; } = null!;
        public virtual Demon? ReviewedBy { get; set; }
    }
}