using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class InitiationRite
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime PerformedDate { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        [MaxLength(500)]
        public string Challenges { get; set; }
        
        public bool WasCompleted { get; set; }
        
        [MaxLength(300)]
        public string Result { get; set; }
        
        public int DifficultyLevel { get; set; }
        
        [MaxLength(500)]
        public string RequiredPreparation { get; set; }
        
        public int FollowerId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
    }
}