using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class LimboOffice
    {
        [Key]
        public int LimboOfficeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string OfficeName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        public int ManagerDemonId { get; set; }

        [MaxLength(500)]
        public string Function { get; set; } = string.Empty;

        public int WorkstationCount { get; set; }

        public int CurrentStaffCount { get; set; }

        public int PendingCasesCount { get; set; }

        public int ProcessedCasesToday { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal AverageProcessingTimeHours { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal EfficiencyRating { get; set; }

        [MaxLength(200)]
        public string ServicesOffered { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OperatingBudget { get; set; }

        [MaxLength(200)]
        public string OfficeHours { get; set; } = string.Empty;

        public bool IsWalkInEnabled { get; set; }

        public bool RequiresAppointment { get; set; }

        [MaxLength(300)]
        public string ProcessingRequirements { get; set; } = string.Empty;

        [MaxLength(200)]
        public string QualityStandards { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Operational";

        public DateTime LastAuditDate { get; set; }

        [MaxLength(500)]
        public string AuditFindings { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Manager { get; set; } = null!;
    }
}