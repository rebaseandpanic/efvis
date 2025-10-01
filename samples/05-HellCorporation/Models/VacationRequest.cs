using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class VacationRequest
    {
        [Key]
        public int VacationRequestId { get; set; }

        public int DemonId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DaysRequested { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Denied

        public int? ApprovedByDemonId { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [MaxLength(500)]
        public string Comments { get; set; } = string.Empty;

        public bool IsEmergency { get; set; }

        [MaxLength(100)]
        public string VacationType { get; set; } = string.Empty; // Annual, Sick, Personal, Sabbatical

        public bool RequiresCoverage { get; set; }

        public int? CoverageByDemonId { get; set; }

        [MaxLength(200)]
        public string Destination { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual Demon? ApprovedBy { get; set; }
        public virtual Demon? CoverageBy { get; set; }
    }
}