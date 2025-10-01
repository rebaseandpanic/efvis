using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class SurvivalGroup
    {
        [Key]
        public int SurvivalGroupId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int MemberCount { get; set; }
        
        public string LeaderName { get; set; } = string.Empty;
        
        public string GroupObjective { get; set; } = string.Empty;
        
        public string CurrentLocation { get; set; } = string.Empty;
        
        public DateTime FormedDate { get; set; }
        
        // Navigation Properties
        public ICollection<Survivor> Members { get; set; } = new List<Survivor>();
    }
}