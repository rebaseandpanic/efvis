using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class DeadCity
    {
        [Key]
        public int DeadCityId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int PreOutbreakPopulation { get; set; }
        
        public int EstimatedZombieCount { get; set; }
        
        public DateTime FallDate { get; set; }
        
        public string DestructionLevel { get; set; } = string.Empty; // Minimal, Moderate, Severe, Complete
        
        public bool HasSurvivors { get; set; }
        
        // Foreign Key
        public int? AttackingHordeId { get; set; }
        
        // Navigation Properties
        public ZombieHorde? AttackingHorde { get; set; }
        public ICollection<AbandonedHospital> AbandonedHospitals { get; set; } = new List<AbandonedHospital>();
        public ICollection<QuarantineZone> QuarantineZones { get; set; } = new List<QuarantineZone>();
        public ICollection<EvacuationPoint> EvacuationPoints { get; set; } = new List<EvacuationPoint>();
    }
}