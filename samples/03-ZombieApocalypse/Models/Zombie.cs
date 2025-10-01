using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Zombie
    {
        [Key]
        public int ZombieId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // Walker, Runner, Crawler, etc.
        
        public int AggessionLevel { get; set; }
        
        public int DecayLevel { get; set; }
        
        public DateTime DeathDate { get; set; }
        
        public bool IsInfectious { get; set; }
        
        public string Location { get; set; } = string.Empty;
        
        // Foreign Keys
        public int? HordeId { get; set; }
        public int? MutationId { get; set; }
        
        // Navigation Properties
        public ZombieHorde? Horde { get; set; }
        public Mutation? Mutation { get; set; }
        public ICollection<Infection> CausedInfections { get; set; } = new List<Infection>();
    }
}