using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class SoulAcquisition
    {
        [Key]
        public int SoulAcquisitionId { get; set; }

        public int AcquisitionAgentDemonId { get; set; }

        public int SinnedSoulId { get; set; }

        public DateTime AcquisitionDate { get; set; }

        [MaxLength(100)]
        public string AcquisitionMethod { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal AcquisitionCost { get; set; }

        [MaxLength(500)]
        public string LocationOfAcquisition { get; set; } = string.Empty;

        [MaxLength(500)]
        public string CircumstancesOfDeath { get; set; } = string.Empty;

        public bool RequiredIntervention { get; set; }

        [MaxLength(300)]
        public string InterventionDetails { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal CommissionEarned { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Completed";

        public bool QualityControlPassed { get; set; }

        [MaxLength(500)]
        public string QCNotes { get; set; } = string.Empty;

        public DateTime ProcessingDate { get; set; }

        public int ProcessedByDemonId { get; set; }

        [MaxLength(200)]
        public string DocumentationRequired { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon AcquisitionAgent { get; set; } = null!;
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual Demon ProcessedBy { get; set; } = null!;
    }
}