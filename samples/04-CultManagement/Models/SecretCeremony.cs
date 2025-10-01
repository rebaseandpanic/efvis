using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SecretCeremony
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime ScheduledDate { get; set; }
        
        [MaxLength(200)]
        public string SecretLocation { get; set; }
        
        public int ParticipantCount { get; set; }
        
        [MaxLength(100)]
        public string SecurityLevel { get; set; }
        
        [MaxLength(500)]
        public string AccessRequirements { get; set; }
        
        [MaxLength(1000)]
        public string CeremonyPurpose { get; set; }
        
        public bool RequiresBloodOath { get; set; }
        
        [MaxLength(500)]
        public string ConsequencesOfDisclosure { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<InnerCircle> Participants { get; set; } = new List<InnerCircle>();
    }
}