using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class DarkMagic
    {
        [Key]
        public int DarkMagicId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string SpellName { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string MagicType { get; set; } = string.Empty;
        
        public int PowerLevel { get; set; }
        
        public int CasterDemonId { get; set; }
        
        [MaxLength(1000)]
        public string Incantation { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Components { get; set; } = string.Empty;
        
        public int CastingTime { get; set; }
        
        [MaxLength(500)]
        public string Effects { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string Restrictions { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string Status { get; set; } = "Available";

        public virtual Demon Caster { get; set; } = null!;
    }
}
