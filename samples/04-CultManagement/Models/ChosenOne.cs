using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class ChosenOne
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public DateTime ChosenDate { get; set; }
        
        [MaxLength(500)]
        public string SpecialPowers { get; set; }
        
        [MaxLength(1000)]
        public string Prophecy { get; set; }
        
        [MaxLength(300)]
        public string Destiny { get; set; }
        
        public bool IsStillAlive { get; set; }
        
        [MaxLength(500)]
        public string ProtectionMeasures { get; set; }
        
        public int PowerLevel { get; set; }
        
        public int? PropheticVisionId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual PropheticVision PropheticVision { get; set; }
    }
}