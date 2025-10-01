using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Incubus
    {
        [Key]
        public int IncubusId { get; set; }

        public int DemonId { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SeductionRating { get; set; }

        public int DreamInvasionCount { get; set; }

        public int SoulsCorruptedThisMonth { get; set; }

        public int PreferredVictimAge { get; set; }

        [MaxLength(100)]
        public string SpecialtyType { get; set; } = string.Empty;

        public bool CanShapeshift { get; set; }

        public DateTime LastHunt { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal EnergyHarvestedToday { get; set; }

        public int ActiveTargets { get; set; }

        [MaxLength(200)]
        public string PreferredHuntingGrounds { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}