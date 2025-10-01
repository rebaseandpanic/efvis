using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class LegalDepartment
    {
        [Key]
        public int LegalDepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        public int ChiefCounselDemonId { get; set; }

        public int TotalLawyers { get; set; }

        public int ActiveCases { get; set; }

        public int CasesWonThisYear { get; set; }

        public int CasesLostThisYear { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal WinRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualBudget { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BillableHoursTarget { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ActualBillableHours { get; set; }

        [MaxLength(200)]
        public string Specializations { get; set; } = string.Empty;

        public int ContractReviewsCompleted { get; set; }

        public int LoopholesExploitedThisYear { get; set; }

        [MaxLength(500)]
        public string CurrentPriorities { get; set; } = string.Empty;

        public DateTime LastAuditDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Operational";

        // Navigation properties
        public virtual Demon ChiefCounsel { get; set; } = null!;
        public virtual ICollection<LoopholeExploit> LoopholeExploits { get; set; } = new List<LoopholeExploit>();
    }
}