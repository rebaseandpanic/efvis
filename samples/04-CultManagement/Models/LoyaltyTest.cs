using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class LoyaltyTest
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string TestName { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime TestDate { get; set; }
        
        [MaxLength(100)]
        public string TestType { get; set; }
        
        [MaxLength(500)]
        public string Challenge { get; set; }
        
        public int DifficultyLevel { get; set; }
        
        public bool WasPassed { get; set; }
        
        public int Score { get; set; }
        
        [MaxLength(1000)]
        public string TestResults { get; set; }
        
        [MaxLength(500)]
        public string ConsequencesOfFailure { get; set; }
        
        public int FollowerId { get; set; }
        public int? RewardSystemId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
        public virtual RewardSystem RewardSystem { get; set; }
    }
}