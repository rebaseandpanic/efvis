using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class InfectedAnimal
    {
        [Key]
        public int InfectedAnimalId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Species { get; set; } = string.Empty;
        
        public string Behavior { get; set; } = string.Empty;
        
        public int AggessionLevel { get; set; }
        
        public bool IsPackAnimal { get; set; }
        
        public DateTime InfectionDate { get; set; }
        
        public string Location { get; set; } = string.Empty;
        
        // Foreign Key
        public int? MutationId { get; set; }
        
        // Navigation Properties
        public Mutation? Mutation { get; set; }
    }
}