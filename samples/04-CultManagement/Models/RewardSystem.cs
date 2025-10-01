using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class RewardSystem
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string RewardName { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string Category { get; set; }
        
        public int PointValue { get; set; }
        
        [MaxLength(500)]
        public string Criteria { get; set; }
        
        [MaxLength(300)]
        public string Benefits { get; set; }
        
        public bool IsMonetary { get; set; }
        
        public decimal MonetaryValue { get; set; }
        
        [MaxLength(200)]
        public string SpecialPrivileges { get; set; }
        
        public bool IsRankPromotion { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<LoyaltyTest> RelatedLoyaltyTests { get; set; } = new List<LoyaltyTest>();
    }
}