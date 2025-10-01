using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Sacrifice
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Type { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public DateTime PerformedDate { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        [MaxLength(300)]
        public string Purpose { get; set; }
        
        public decimal Value { get; set; }
        
        [MaxLength(200)]
        public string Method { get; set; }
        
        public bool WasSuccessful { get; set; }
        
        [MaxLength(500)]
        public string Outcome { get; set; }
        
        public int RitualId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Ritual Ritual { get; set; }
    }
}