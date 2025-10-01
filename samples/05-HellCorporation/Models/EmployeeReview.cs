using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class EmployeeReview
    {
        [Key]
        public int EmployeeReviewId { get; set; }

        public int DemonId { get; set; }

        public int ReviewerDemonId { get; set; }

        public int HellHRId { get; set; }

        public DateTime ReviewDate { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal OverallRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal EvilnessRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal EfficiencyRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal TeamworkRating { get; set; }

        [MaxLength(1000)]
        public string Strengths { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string AreasForImprovement { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Goals { get; set; } = string.Empty;

        public bool RecommendedForPromotion { get; set; }

        public bool RequiresAdditionalTraining { get; set; }

        [MaxLength(500)]
        public string Comments { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual Demon Reviewer { get; set; } = null!;
        public virtual HellHR HellHR { get; set; } = null!;
    }
}