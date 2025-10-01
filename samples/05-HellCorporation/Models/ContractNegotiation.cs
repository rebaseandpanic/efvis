using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class ContractNegotiation
    {
        [Key]
        public int ContractNegotiationId { get; set; }

        public int SoulContractId { get; set; }

        public int NegotiatorDemonId { get; set; }

        public DateTime NegotiationStartDate { get; set; }

        public DateTime? NegotiationEndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "InProgress"; // InProgress, Completed, Failed, Stalled

        [MaxLength(500)]
        public string InitialTerms { get; set; } = string.Empty;

        [MaxLength(500)]
        public string FinalTerms { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal InitialOffer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalOffer { get; set; }

        public int CounterOffers { get; set; }

        [MaxLength(1000)]
        public string SoulDemands { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string HellConcessions { get; set; } = string.Empty;

        public bool RequiredLegalReview { get; set; }

        public int? LegalReviewerDemonId { get; set; }

        public DateTime? LegalReviewDate { get; set; }

        [MaxLength(500)]
        public string StickingPoints { get; set; } = string.Empty;

        [MaxLength(500)]
        public string NegotiationNotes { get; set; } = string.Empty;

        public int Priority { get; set; } // 1-10

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual Demon Negotiator { get; set; } = null!;
        public virtual Demon? LegalReviewer { get; set; }
    }
}