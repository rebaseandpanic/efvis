using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Survivor
    {
        [Key]
        public int SurvivorId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int Age { get; set; }
        
        public string SkillSet { get; set; } = string.Empty;
        
        public int HealthLevel { get; set; }
        
        public int SurvivalDays { get; set; }
        
        public bool IsImmune { get; set; }
        
        // Foreign Keys
        public int? SafeHouseId { get; set; }
        public int? SurvivalGroupId { get; set; }
        
        // Navigation Properties
        public SafeHouse? SafeHouse { get; set; }
        public SurvivalGroup? SurvivalGroup { get; set; }
        public ICollection<Infection> Infections { get; set; } = new List<Infection>();
        public ICollection<RadioTransmission> SentTransmissions { get; set; } = new List<RadioTransmission>();
    }
}