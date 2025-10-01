using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Mutation
    {
        [Key]
        public int MutationId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        
        public int Severity { get; set; }
        
        public bool IsContagious { get; set; }
        
        public string Symptoms { get; set; } = string.Empty;
        
        public DateTime DiscoveryDate { get; set; }
        
        // Foreign Key
        public int? OriginLabId { get; set; }
        
        // Navigation Properties
        public BioweaponLab? OriginLab { get; set; }
        public ICollection<Zombie> MutatedZombies { get; set; } = new List<Zombie>();
        public ICollection<InfectedAnimal> MutatedAnimals { get; set; } = new List<InfectedAnimal>();
    }
}