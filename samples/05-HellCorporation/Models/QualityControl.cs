using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class QualityControl
    {
        [Key]
        public int QualityControlId { get; set; }

        public int TortureDepartmentId { get; set; }

        public int InspectorDemonId { get; set; }

        public DateTime InspectionDate { get; set; }

        [MaxLength(50)]
        public string InspectionType { get; set; } = string.Empty; // Routine, Random, Complaint, Follow-up

        [MaxLength(20)]
        public string OverallRating { get; set; } = string.Empty; // Excellent, Good, Satisfactory, Needs Improvement, Unsatisfactory

        [Column(TypeName = "decimal(5,2)")]
        public decimal QualityScore { get; set; }

        [MaxLength(1000)]
        public string Findings { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Strengths { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Deficiencies { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Recommendations { get; set; } = string.Empty;

        public bool RequiresFollowUp { get; set; }

        public DateTime? FollowUpDate { get; set; }

        [MaxLength(500)]
        public string CorrectiveActions { get; set; } = string.Empty;

        public DateTime? CorrectiveActionDeadline { get; set; }

        [MaxLength(20)]
        public string ComplianceStatus { get; set; } = "Compliant";

        [MaxLength(500)]
        public string NonComplianceIssues { get; set; } = string.Empty;

        public bool CertificationMaintained { get; set; } = true;

        [MaxLength(500)]
        public string AdditionalComments { get; set; } = string.Empty;

        public DateTime? ReportSubmissionDate { get; set; }

        // Navigation properties
        public virtual TortureDepartment TortureDepartment { get; set; } = null!;
        public virtual Demon Inspector { get; set; } = null!;
    }
}