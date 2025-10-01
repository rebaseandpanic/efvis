using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Bunker
    {
        [Key]
        public int BunkerId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public int DepthMeters { get; set; }
        
        public int Capacity { get; set; }
        
        public bool HasSelfSustaining { get; set; }
        
        public DateTime ConstructionDate { get; set; }
        
        // Navigation Properties
        public ICollection<LastHuman> LastHumans { get; set; } = new List<LastHuman>();
        public ICollection<Vaccine> StoredVaccines { get; set; } = new List<Vaccine>();
    }
}