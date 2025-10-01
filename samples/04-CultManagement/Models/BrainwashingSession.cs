using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class BrainwashingSession
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string SessionName { get; set; }
        
        public DateTime SessionDate { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        [MaxLength(100)]
        public string Method { get; set; }
        
        [MaxLength(1000)]
        public string Techniques { get; set; }
        
        public int IntensityLevel { get; set; }
        
        [MaxLength(500)]
        public string TargetedBeliefs { get; set; }
        
        [MaxLength(500)]
        public string ExpectedOutcome { get; set; }
        
        public bool WasSuccessful { get; set; }
        
        [MaxLength(1000)]
        public string SessionNotes { get; set; }
        
        public int FollowerId { get; set; }
        public int? MindControlTechniqueId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
        public virtual MindControlTechnique MindControlTechnique { get; set; }
    }
}