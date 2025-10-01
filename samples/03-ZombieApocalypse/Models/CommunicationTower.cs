using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class CommunicationTower
    {
        [Key]
        public int CommunicationTowerId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string CallSign { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public double RangeKilometers { get; set; }
        
        public bool IsOperational { get; set; }
        
        public string FrequencyRange { get; set; } = string.Empty;
        
        public DateTime LastMaintenance { get; set; }
        
        // Navigation Properties
        public ICollection<RadioTransmission> Transmissions { get; set; } = new List<RadioTransmission>();
    }
}