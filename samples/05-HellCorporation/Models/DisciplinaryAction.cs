using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DisciplinaryAction
    {
        [Key]
        public int DisciplinaryActionId { get; set; }

        public int DemonId { get; set; }

        public int IssuedByDemonId { get; set; }

        public DateTime IssueDate { get; set; }

        [MaxLength(50)]
        public string ActionType { get; set; } = string.Empty; // Warning, Suspension, Termination

        [MaxLength(1000)]
        public string Infraction { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Details { get; set; } = string.Empty;

        public int SeverityLevel { get; set; } // 1-10

        public int SuspensionDays { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FineAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? ExpirationDate { get; set; }

        [MaxLength(500)]
        public string ImprovementRequired { get; set; } = string.Empty;

        public bool AppealAllowed { get; set; } = true;

        public DateTime? AppealDeadline { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
        public virtual Demon IssuedBy { get; set; } = null!;
    }
}