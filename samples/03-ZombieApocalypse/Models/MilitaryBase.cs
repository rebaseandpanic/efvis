using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class MilitaryBase
    {
        [Key]
        public int MilitaryBaseId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Coordinates { get; set; } = string.Empty;
        
        public string Status { get; set; } = string.Empty; // Active, Abandoned, Overrun, Destroyed
        
        public int DefenseLevel { get; set; }
        
        public bool HasAirSupport { get; set; }
        
        public DateTime LastContactDate { get; set; }
        
        // Navigation Properties
        public ICollection<WeaponCache> WeaponCaches { get; set; } = new List<WeaponCache>();
        public ICollection<Helicopter> Helicopters { get; set; } = new List<Helicopter>();
        public ICollection<RadioTransmission> Transmissions { get; set; } = new List<RadioTransmission>();
        public ICollection<SupplyDrop> SupplyDrops { get; set; } = new List<SupplyDrop>();
    }
}