using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class ZombieHorde
    {
        [Key]
        public int HordeId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public int Size { get; set; }
        
        public string CurrentLocation { get; set; } = string.Empty;
        
        public string MovementDirection { get; set; } = string.Empty;
        
        public int ThreatLevel { get; set; }
        
        public DateTime LastSightedDate { get; set; }
        
        // Navigation Properties
        public ICollection<Zombie> Zombies { get; set; } = new List<Zombie>();
        public ICollection<DeadCity> AttackedCities { get; set; } = new List<DeadCity>();
    }
}