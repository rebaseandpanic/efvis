using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class SoulProcessing
    {
        [Key]
        public int SoulProcessingId { get; set; }

        public int SinnedSoulId { get; set; }

        public int SoulWarehouseId { get; set; }

        public int ProcessorDemonId { get; set; }

        public DateTime ProcessingStartDate { get; set; }

        public DateTime? ProcessingEndDate { get; set; }

        [MaxLength(50)]
        public string ProcessingType { get; set; } = string.Empty; // Initial, Reprocessing, Upgrade, Downgrade

        [MaxLength(20)]
        public string Status { get; set; } = "InProgress";

        [MaxLength(500)]
        public string ProcessingSteps { get; set; } = string.Empty;

        public int CurrentStep { get; set; }

        public int TotalSteps { get; set; }

        [MaxLength(500)]
        public string QualityCheckResults { get; set; } = string.Empty;

        public bool PassedQualityControl { get; set; }

        [MaxLength(500)]
        public string ProcessingNotes { get; set; } = string.Empty;

        public bool RequiresSpecialHandling { get; set; }

        [MaxLength(300)]
        public string SpecialInstructions { get; set; } = string.Empty;

        public int Priority { get; set; } // 1-10

        public DateTime? QualityControlDate { get; set; }

        public int? QualityControlByDemonId { get; set; }

        [MaxLength(200)]
        public string DefectsFound { get; set; } = string.Empty;

        // Navigation properties
        public virtual SinnedSoul SinnedSoul { get; set; } = null!;
        public virtual SoulWarehouse SoulWarehouse { get; set; } = null!;
        public virtual Demon Processor { get; set; } = null!;
        public virtual Demon? QualityControlBy { get; set; }
    }
}