using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class DamnationClause
    {
        [Key]
        public int DamnationClauseId { get; set; }

        public int EternalContractId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClauseNumber { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string ClauseText { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ClauseType { get; set; } = string.Empty;

        public int SeverityLevel { get; set; } // 1-10

        public bool IsEnforceable { get; set; } = true;

        [MaxLength(500)]
        public string TriggerConditions { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Consequences { get; set; } = string.Empty;

        public bool RequiresWitnessing { get; set; }

        public int MinimumWitnesses { get; set; }

        [MaxLength(200)]
        public string ExemptionConditions { get; set; } = string.Empty;

        public DateTime LastInvoked { get; set; }

        public int InvocationCount { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(300)]
        public string LegalPrecedent { get; set; } = string.Empty;

        // Navigation properties
        public virtual EternalContract EternalContract { get; set; } = null!;
    }
}