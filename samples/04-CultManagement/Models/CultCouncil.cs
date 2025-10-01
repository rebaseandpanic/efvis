using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class CultCouncil
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Purpose { get; set; }
        
        public DateTime FormedDate { get; set; }
        
        public int MemberCount { get; set; }
        
        [MaxLength(100)]
        public string MeetingFrequency { get; set; }
        
        public bool IsSecretCouncil { get; set; }
        
        [MaxLength(300)]
        public string DecisionMakingPower { get; set; }
        
        public DateTime LastMeetingDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<InnerCircle> Members { get; set; } = new List<InnerCircle>();
        public virtual ICollection<SecretMeetingPlace> MeetingPlaces { get; set; } = new List<SecretMeetingPlace>();
    }
}