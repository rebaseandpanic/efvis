using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class QuarantineZone
    {
        [Key]
        public int QuarantineZoneId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Perimeter { get; set; } = string.Empty;
        
        public bool IsBreached { get; set; }
        
        public int SecurityLevel { get; set; }
        
        public DateTime EstablishedDate { get; set; }
        
        public string Status { get; set; } = string.Empty; // Active, Breached, Abandoned, Cleared
        
        // Foreign Key
        public int? DeadCityId { get; set; }
        
        // Navigation Properties
        public DeadCity? DeadCity { get; set; }
    }
}