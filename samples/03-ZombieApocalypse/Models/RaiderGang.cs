using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class RaiderGang
    {
        [Key]
        public int RaiderGangId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int MemberCount { get; set; }
        
        public string Territory { get; set; } = string.Empty;
        
        public string LeaderName { get; set; } = string.Empty;
        
        public int ThreatLevel { get; set; }
        
        public string Tactics { get; set; } = string.Empty;
        
        // Navigation Properties
        public ICollection<Cannibal> Members { get; set; } = new List<Cannibal>();
    }
}