using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class IsolationPeriod
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Reason { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        [MaxLength(100)]
        public string IsolationType { get; set; }
        
        [MaxLength(500)]
        public string Conditions { get; set; }
        
        [MaxLength(300)]
        public string AllowedActivities { get; set; }
        
        [MaxLength(300)]
        public string ProhibitedActivities { get; set; }
        
        public bool HasVisitationRights { get; set; }
        
        [MaxLength(1000)]
        public string Notes { get; set; }
        
        public int SubjectId { get; set; }
        public int? ThoughtReformId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Subject { get; set; }
        public virtual ThoughtReform ThoughtReform { get; set; }
    }
}