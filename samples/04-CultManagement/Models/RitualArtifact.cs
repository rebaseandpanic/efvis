using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class RitualArtifact
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }
        
        [MaxLength(200)]
        public string Material { get; set; }
        
        public DateTime AcquiredDate { get; set; }
        
        [MaxLength(300)]
        public string Origin { get; set; }
        
        public decimal EstimatedValue { get; set; }
        
        [MaxLength(500)]
        public string MagicalProperties { get; set; }
        
        [MaxLength(200)]
        public string CurrentLocation { get; set; }
        
        public bool IsConsecrated { get; set; }
        
        [MaxLength(300)]
        public string UsageInstructions { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Ritual> UsedInRituals { get; set; } = new List<Ritual>();
    }
}