using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class EternalContract
    {
        [Key]
        public int EternalContractId { get; set; }

        public int SoulContractId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EternalClauseNumber { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string EternalTerms { get; set; } = string.Empty;

        public bool IsIrrevocable { get; set; } = true;

        public bool PermitsAppeal { get; set; }

        public int AppealWindowDays { get; set; }

        [MaxLength(500)]
        public string EternalPunishments { get; set; } = string.Empty;

        [MaxLength(200)]
        public string EscapeConditions { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal CompoundingFactor { get; set; }

        public DateTime LastCompoundingDate { get; set; }

        [MaxLength(500)]
        public string EternalObligations { get; set; } = string.Empty;

        public bool RequiresSoulBond { get; set; }

        [MaxLength(200)]
        public string BondingRitualRequired { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public int AuthorizedByArchDemonId { get; set; }

        // Navigation properties
        public virtual SoulContract SoulContract { get; set; } = null!;
        public virtual ArchDemon AuthorizedByArchDemon { get; set; } = null!;
        public virtual ICollection<DamnationClause> DamnationClauses { get; set; } = new List<DamnationClause>();
    }
}