using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Succubus
    {
        [Key]
        public int SuccubusId { get; set; }

        public int DemonId { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CharmRating { get; set; }

        public int VictimsThisWeek { get; set; }

        public int LifeForceExtracted { get; set; }

        [MaxLength(100)]
        public string PreferredDisguise { get; set; } = string.Empty;

        public bool CanManipulateEmotion { get; set; }

        public DateTime LastSuccessfulHunt { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal VitalityHarvestedToday { get; set; }

        public int IllusionsPowerLevel { get; set; }

        [MaxLength(200)]
        public string TerritoryAssignment { get; set; } = string.Empty;

        public bool IsInTemptationMode { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}