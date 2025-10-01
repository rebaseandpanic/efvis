using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class ConfessionSession
    {
        public int Id { get; set; }
        
        public DateTime SessionDate { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }
        
        [MaxLength(2000)]
        public string ConfessionContent { get; set; }
        
        [MaxLength(500)]
        public string Sins { get; set; }
        
        [MaxLength(500)]
        public string Penance { get; set; }
        
        public bool WasVoluntary { get; set; }
        
        [MaxLength(200)]
        public string Confessor { get; set; }
        
        [MaxLength(1000)]
        public string SessionNotes { get; set; }
        
        public bool RequiresFollowUp { get; set; }
        
        public int FollowerId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
        public virtual ICollection<Punishment> RelatedPunishments { get; set; } = new List<Punishment>();
    }
}