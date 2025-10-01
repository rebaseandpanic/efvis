using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class BloodOath
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string OathName { get; set; }
        
        [MaxLength(1000)]
        public string OathText { get; set; }
        
        public DateTime TakenDate { get; set; }
        
        [MaxLength(200)]
        public string Witness { get; set; }
        
        [MaxLength(500)]
        public string BloodSource { get; set; }
        
        [MaxLength(300)]
        public string Consequences { get; set; }
        
        public bool IsBinding { get; set; }
        
        public bool HasBeenBroken { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
        
        [MaxLength(500)]
        public string BreakingPenalty { get; set; }
        
        public int FollowerId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
    }
}