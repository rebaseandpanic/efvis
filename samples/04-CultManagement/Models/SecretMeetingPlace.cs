using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SecretMeetingPlace
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string CodeName { get; set; }
        
        [MaxLength(300)]
        public string Location { get; set; }
        
        [MaxLength(100)]
        public string MeetingType { get; set; }
        
        public int MaxAttendees { get; set; }
        
        [MaxLength(500)]
        public string SecurityFeatures { get; set; }
        
        [MaxLength(300)]
        public string AccessInstructions { get; set; }
        
        [MaxLength(500)]
        public string CommunicationEquipment { get; set; }
        
        public bool IsCounterSurveillanceSecure { get; set; }
        
        [MaxLength(300)]
        public string EscapeRoutes { get; set; }
        
        public DateTime LastUsedDate { get; set; }
        
        public bool IsCompromised { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<CultCouncil> Councils { get; set; } = new List<CultCouncil>();
    }
}