using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Punishment
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Type { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime IssuedDate { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        
        [MaxLength(500)]
        public string Reason { get; set; }
        
        public int SeverityLevel { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        [MaxLength(200)]
        public string IssuedBy { get; set; }
        
        [MaxLength(500)]
        public string Conditions { get; set; }
        
        public bool WasCompleted { get; set; }
        
        public int FollowerId { get; set; }
        public int? ConfessionSessionId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
        public virtual ConfessionSession ConfessionSession { get; set; }
    }
}