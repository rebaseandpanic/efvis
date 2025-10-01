using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Vaccine
    {
        [Key]
        public int VaccineId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public string Formula { get; set; } = string.Empty;
        
        public int EffectivenessPercent { get; set; }
        
        public string Stage { get; set; } = string.Empty; // Research, Testing, Production, Deployed
        
        public DateTime DevelopmentDate { get; set; }
        
        public int DosesAvailable { get; set; }
        
        // Foreign Keys
        public int? DeveloperLabId { get; set; }
        public int? StorageBunkerId { get; set; }
        
        // Navigation Properties
        public BioweaponLab? DeveloperLab { get; set; }
        public Bunker? StorageBunker { get; set; }
    }
}