using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class EvacuationPoint
    {
        [Key]
        public int EvacuationPointId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Coordinates { get; set; } = string.Empty;
        
        public int Capacity { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime LastEvacuation { get; set; }
        
        public string Status { get; set; } = string.Empty; // Operational, Compromised, Overrun, Abandoned
        
        // Foreign Key
        public int? DeadCityId { get; set; }
        
        // Navigation Properties
        public DeadCity? DeadCity { get; set; }
        public ICollection<Helicopter> ServicingHelicopters { get; set; } = new List<Helicopter>();
    }
}