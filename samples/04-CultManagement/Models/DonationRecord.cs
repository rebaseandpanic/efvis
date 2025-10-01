using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class DonationRecord
    {
        public int Id { get; set; }
        
        public DateTime DonationDate { get; set; }
        
        public decimal Amount { get; set; }
        
        [MaxLength(100)]
        public string Currency { get; set; }
        
        [MaxLength(100)]
        public string DonationType { get; set; }
        
        [MaxLength(500)]
        public string Purpose { get; set; }
        
        [MaxLength(100)]
        public string PaymentMethod { get; set; }
        
        public bool IsRecurring { get; set; }
        
        [MaxLength(100)]
        public string RecurrenceFrequency { get; set; }
        
        public bool WasCoerced { get; set; }
        
        [MaxLength(1000)]
        public string Notes { get; set; }
        
        public int FollowerId { get; set; }
        public int? PyramidSchemeId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Follower Follower { get; set; }
        public virtual PyramidScheme PyramidScheme { get; set; }
    }
}