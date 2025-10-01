using System.ComponentModel.DataAnnotations;

namespace HellCorporation.Models
{
    public class Hellhound
    {
        [Key]
        public int HellhoundId { get; set; }

        public int DemonId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BreedType { get; set; } = string.Empty;

        public int PackSize { get; set; }

        public bool CanBreatheFire { get; set; }

        public int HuntingSuccessRate { get; set; }

        [MaxLength(100)]
        public string PrimaryTerritory { get; set; } = string.Empty;

        public DateTime LastHunt { get; set; }

        public int EscapedSoulsCaught { get; set; }

        public bool IsAlphaOfPack { get; set; }

        public int LoyaltyLevel { get; set; }

        [MaxLength(200)]
        public string SpecialAbilities { get; set; } = string.Empty;

        public int TrackingRange { get; set; }

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}