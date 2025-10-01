using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class HellHR
    {
        [Key]
        public int HellHRId { get; set; }

        public int HRManagerDemonId { get; set; }

        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;

        public int TotalEmployees { get; set; }

        public int NewHiresThisMonth { get; set; }

        public int TerminationsThisMonth { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal TurnoverRate { get; set; }

        public int OpenPositions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPayrollBudget { get; set; }

        public int TrainingProgramsActive { get; set; }

        public int DisciplinaryActionsThisMonth { get; set; }

        public int UnionGrievances { get; set; }

        public DateTime LastAuditDate { get; set; }

        // Navigation properties
        public virtual Demon HRManager { get; set; } = null!;
        public virtual ICollection<EmployeeReview> EmployeeReviews { get; set; } = new List<EmployeeReview>();
        public virtual ICollection<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();
    }
}