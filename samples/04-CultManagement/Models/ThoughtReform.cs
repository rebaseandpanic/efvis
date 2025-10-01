using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class ThoughtReform
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string ProgramName { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [MaxLength(500)]
        public string TargetedThoughts { get; set; }
        
        [MaxLength(500)]
        public string DesiredBeliefs { get; set; }
        
        [MaxLength(300)]
        public string Phase { get; set; }
        
        public int IntensityLevel { get; set; }
        
        [MaxLength(500)]
        public string Techniques { get; set; }
        
        public int ProgressPercentage { get; set; }
        
        public int MindControlTechniqueId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual MindControlTechnique MindControlTechnique { get; set; }
        public virtual ICollection<IsolationPeriod> IsolationPeriods { get; set; } = new List<IsolationPeriod>();
    }
}