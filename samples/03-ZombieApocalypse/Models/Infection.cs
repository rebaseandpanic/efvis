using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Infection
    {
        [Key]
        public int InfectionId { get; set; }
        
        public DateTime InfectionDate { get; set; }
        
        public int IncubationPeriodHours { get; set; }
        
        public int SeverityLevel { get; set; }
        
        public bool IsTreatable { get; set; }
        
        public string InfectionMethod { get; set; } = string.Empty; // Bite, Scratch, Airborne, etc.
        
        public string CurrentStatus { get; set; } = string.Empty; // Incubating, Active, Treated, Terminal
        
        // Foreign Keys
        public int SurvivorId { get; set; }
        public int? CauseZombieId { get; set; }
        
        // Navigation Properties
        public Survivor Survivor { get; set; } = null!;
        public Zombie? CauseZombie { get; set; }
    }
}