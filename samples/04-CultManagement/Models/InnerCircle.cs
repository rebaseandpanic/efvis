using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class InnerCircle
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [MaxLength(100)]
        public string Position { get; set; }
        
        public DateTime JoinedInnerCircleDate { get; set; }
        
        public int TrustLevel { get; set; }
        
        [MaxLength(500)]
        public string Responsibilities { get; set; }
        
        public bool HasAccessToSecrets { get; set; }
        
        [MaxLength(200)]
        public string SecurityClearance { get; set; }
        
        public int CultLeaderId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CultLeader CultLeader { get; set; }
        public virtual ICollection<SecretCeremony> SecretCeremonies { get; set; } = new List<SecretCeremony>();
        public virtual ICollection<CultCouncil> CouncilMemberships { get; set; } = new List<CultCouncil>();
    }
}