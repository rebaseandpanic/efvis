using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class RetirementPlan
    {
        [Key]
        public int RetirementPlanId { get; set; }

        public int DemonId { get; set; }

        [MaxLength(50)]
        public string PlanType { get; set; } = string.Empty; // 401(k), Pension, IRA

        [Column(TypeName = "decimal(18,2)")]
        public decimal ContributionAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ContributionPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CompanyMatch { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VestedAmount { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public DateTime? VestingDate { get; set; }

        public int YearsOfService { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal VestingPercentage { get; set; }

        public DateTime? RetirementDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        [MaxLength(200)]
        public string InvestmentOptions { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}