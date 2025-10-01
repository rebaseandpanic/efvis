using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class Follower
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(200)]
        public string ContactEmail { get; set; }
        
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        
        public DateTime JoinedDate { get; set; }
        
        public int DevotionLevel { get; set; }
        
        [MaxLength(200)]
        public string RecruitmentSource { get; set; }
        
        public decimal MonthlyContribution { get; set; }
        
        public bool HasCompletedInitiation { get; set; }
        
        [MaxLength(500)]
        public string SpecialSkills { get; set; }
        
        public int CultId { get; set; }
        public int? CultRankId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Cult Cult { get; set; }
        public virtual CultRank CultRank { get; set; }
        public virtual ICollection<InitiationRite> InitiationRites { get; set; } = new List<InitiationRite>();
        public virtual ICollection<BrainwashingSession> BrainwashingSessions { get; set; } = new List<BrainwashingSession>();
        public virtual ICollection<LoyaltyTest> LoyaltyTests { get; set; } = new List<LoyaltyTest>();
        public virtual ICollection<DonationRecord> Donations { get; set; } = new List<DonationRecord>();
    }
}