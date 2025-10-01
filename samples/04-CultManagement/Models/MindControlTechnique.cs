using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class MindControlTechnique
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string Category { get; set; }
        
        public int EffectivenessRating { get; set; }
        
        [MaxLength(500)]
        public string TargetPsychology { get; set; }
        
        [MaxLength(500)]
        public string Implementation { get; set; }
        
        [MaxLength(300)]
        public string RequiredResources { get; set; }
        
        public TimeSpan TypicalDuration { get; set; }
        
        [MaxLength(500)]
        public string SideEffects { get; set; }
        
        public bool RequiresSpecialTraining { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<BrainwashingSession> BrainwashingSessions { get; set; } = new List<BrainwashingSession>();
        public virtual ICollection<ThoughtReform> ThoughtReforms { get; set; } = new List<ThoughtReform>();
    }
}