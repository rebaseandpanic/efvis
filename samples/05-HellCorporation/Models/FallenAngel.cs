using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class FallenAngel
    {
        [Key]
        public int FallenAngelId { get; set; }

        public int DemonId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FormerHeavenlyName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string AngelicOrder { get; set; } = string.Empty;

        public DateTime DateOfFall { get; set; }

        [MaxLength(500)]
        public string ReasonForFall { get; set; } = string.Empty;

        public int WingsCount { get; set; } = 6;

        public bool RetainsHolyPowers { get; set; }

        public bool CanFlyBetweenRealms { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CorruptionLevel { get; set; }

        [MaxLength(200)]
        public string LostAbilities { get; set; } = string.Empty;

        [MaxLength(200)]
        public string GainedAbilities { get; set; } = string.Empty;

        // Navigation properties
        public virtual Demon Demon { get; set; } = null!;
    }
}