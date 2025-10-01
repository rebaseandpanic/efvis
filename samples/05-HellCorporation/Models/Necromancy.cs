using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellCorporation.Models
{
    public class Necromancy
    {
        [Key]
        public int NecromancyId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string SpellName { get; set; } = string.Empty;
        
        public int NecromancerDemonId { get; set; }
        
        [MaxLength(100)]
        public string TargetType { get; set; } = string.Empty;
        
        public DateTime CastDate { get; set; }
        
        [MaxLength(500)]
        public string Results { get; set; } = string.Empty;
        
        public int PowerRequired { get; set; }
        
        [MaxLength(20)]
        public string Status { get; set; } = "Completed";

        public virtual Demon Necromancer { get; set; } = null!;
    }
}
