using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class BioweaponLab
    {
        [Key]
        public int BioweaponLabId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public int SecurityClearance { get; set; }
        
        public bool IsContained { get; set; }
        
        public DateTime LastActivity { get; set; }
        
        public string ResearchFocus { get; set; } = string.Empty;
        
        // Foreign Key
        public int? RadiationZoneId { get; set; }
        
        // Navigation Properties
        public RadiationZone? RadiationZone { get; set; }
        public ICollection<Mutation> CreatedMutations { get; set; } = new List<Mutation>();
        public ICollection<Vaccine> DevelopedVaccines { get; set; } = new List<Vaccine>();
    }
}