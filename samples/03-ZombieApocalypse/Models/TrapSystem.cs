using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class TrapSystem
    {
        [Key]
        public int TrapSystemId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string TrapType { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public bool IsActive { get; set; }
        
        public int EffectivenessRating { get; set; }
        
        public DateTime InstallationDate { get; set; }
        
        public int ZombiesTrapped { get; set; }
        
        // Foreign Key
        public int? SafeHouseId { get; set; }
        
        // Navigation Properties
        public SafeHouse? SafeHouse { get; set; }
    }
}