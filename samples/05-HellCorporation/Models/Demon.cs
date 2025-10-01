using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Demon
    {
        [Key]
        public int DemonId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TrueName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Species { get; set; } = string.Empty;

        public int PowerLevel { get; set; }

        public DateTime DateOfCorruption { get; set; }

        public DateTime HireDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public int DemonRankId { get; set; }
        public int DemonDepartmentId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentSalary { get; set; }

        public bool IsUnionMember { get; set; }

        // Navigation properties
        public virtual DemonRank DemonRank { get; set; } = null!;
        public virtual DemonDepartment DemonDepartment { get; set; } = null!;
        public virtual ICollection<EmployeeReview> EmployeeReviews { get; set; } = new List<EmployeeReview>();
        public virtual ICollection<DemonContract> DemonContracts { get; set; } = new List<DemonContract>();
        public virtual ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
        public virtual ICollection<SickLeave> SickLeaves { get; set; } = new List<SickLeave>();
        public virtual ICollection<DisciplinaryAction> DisciplinaryActions { get; set; } = new List<DisciplinaryAction>();
    }
}