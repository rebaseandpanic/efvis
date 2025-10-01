using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Cannibal
    {
        [Key]
        public int CannibalId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int ThreatLevel { get; set; }
        
        public string Territory { get; set; } = string.Empty;
        
        public bool IsLeader { get; set; }
        
        public int KillCount { get; set; }
        
        public string PreferredWeapon { get; set; } = string.Empty;
        
        // Foreign Key
        public int? RaiderGangId { get; set; }
        
        // Navigation Properties
        public RaiderGang? RaiderGang { get; set; }
    }
}