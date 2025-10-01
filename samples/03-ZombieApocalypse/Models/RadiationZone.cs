using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class RadiationZone
    {
        [Key]
        public int RadiationZoneId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Coordinates { get; set; } = string.Empty;
        
        public double RadiationLevel { get; set; }
        
        public double RadiusKilometers { get; set; }
        
        public DateTime ContaminationDate { get; set; }
        
        public string ContaminationSource { get; set; } = string.Empty;
        
        // Navigation Properties
        public ICollection<BioweaponLab> BioweaponLabs { get; set; } = new List<BioweaponLab>();
        public ICollection<MutantBoss> MutantBosses { get; set; } = new List<MutantBoss>();
    }
}