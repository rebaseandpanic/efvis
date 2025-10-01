using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class MutantBoss
    {
        [Key]
        public int MutantBossId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public string MutationType { get; set; } = string.Empty;
        
        public int PowerLevel { get; set; }
        
        public string SpecialAbilities { get; set; } = string.Empty;
        
        public string Weakness { get; set; } = string.Empty;
        
        public bool IsDefeated { get; set; }
        
        // Foreign Key
        public int? RadiationZoneId { get; set; }
        
        // Navigation Properties
        public RadiationZone? RadiationZone { get; set; }
    }
}