using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class LastHuman
    {
        [Key]
        public int LastHumanId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int Age { get; set; }
        
        public string Profession { get; set; } = string.Empty;
        
        public int SurvivalDays { get; set; }
        
        public bool IsImmune { get; set; }
        
        public string PersonalLog { get; set; } = string.Empty;
        
        // Foreign Key
        public int? BunkerId { get; set; }
        
        // Navigation Properties
        public Bunker? Bunker { get; set; }
    }
}