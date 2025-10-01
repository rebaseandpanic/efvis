using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class SickLeave
    {
        [Key]
        public int SickLeaveId { get; set; }

        public int DemonId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int DaysUsed { get; set; }

        [MaxLength(200)]
        public string Illness { get; set; } = string.Empty;

        [MaxLength(100)]
        public string DoctorName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Extended

        public bool RequiresMedicalCertificate { get; set; }

        public bool CertificateProvided { get; set; }

        public DateTime? ExpectedReturnDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;

        public bool IsWorkRelated { get; set; }

        [MaxLength(200)]
        public string Treatment { get; set; } = string.Empty;

        public bool RequiresAccommodation { get; set; }

        [MaxLength(300)]
        public string AccommodationDetails { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}