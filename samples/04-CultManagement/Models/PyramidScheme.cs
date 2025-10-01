using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class PyramidScheme
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string SchemeName { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime LaunchDate { get; set; }
        
        public DateTime? CollapseDate { get; set; }
        
        public int TotalParticipants { get; set; }
        
        public int Levels { get; set; }
        
        public decimal InitialInvestment { get; set; }
        
        public decimal PromisedReturns { get; set; }
        
        public decimal ActualReturns { get; set; }
        
        [MaxLength(500)]
        public string RecruitmentIncentives { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        public bool WasShutDown { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<DonationRecord> RelatedDonations { get; set; } = new List<DonationRecord>();
    }
}