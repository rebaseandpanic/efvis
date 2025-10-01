using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class CustomerSatisfaction
    {
        [Key]
        public int CustomerSatisfactionId { get; set; }

        public int SinnedSoulId { get; set; }

        public int TortureDepartmentId { get; set; }

        public DateTime SurveyDate { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal OverallSatisfactionRating { get; set; } // 1.0-10.0

        [Column(TypeName = "decimal(3,1)")]
        public decimal PainQualityRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal SufferingIntensityRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal TorturerProfessionalismRating { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal FacilityConditionsRating { get; set; }

        [MaxLength(1000)]
        public string Comments { get; set; } = string.Empty;

        [MaxLength(500)]
        public string SuggestionsForImprovement { get; set; } = string.Empty;

        [MaxLength(500)]
        public string MostSatisfyingAspects { get; set; } = string.Empty;

        [MaxLength(500)]
        public string LeastSatisfyingAspects { get; set; } = string.Empty;

        public bool WouldRecommendDepartment { get; set; }

        public bool ExceededExpectations { get; set; }

        [MaxLength(300)]
        public string ComparedToPreviousExperience { get; set; } = string.Empty;

        [MaxLength(200)]
        public string PreferredTortureTypes { get; set; } = string.Empty;

        public bool WillingToParticipateInFutureStudies { get; set; }

        [MaxLength(500)]
        public string AdditionalFeedback { get; set; } = string.Empty;

        public int ProcessedByDemonId { get; set; }

        [MaxLength(300)]
        public string ProcessingNotes { get; set; } = string.Empty;

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual Demon ProcessedBy { get; set; } = null!;
    }
}