using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SecurityDetail
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string DetailName { get; set; }
        
        [MaxLength(100)]
        public string SecurityLevel { get; set; }
        
        public int PersonnelCount { get; set; }
        
        [MaxLength(500)]
        public string Responsibilities { get; set; }
        
        [MaxLength(300)]
        public string Equipment { get; set; }
        
        [MaxLength(200)]
        public string LeadOfficer { get; set; }
        
        [MaxLength(300)]
        public string OperatingProcedures { get; set; }
        
        [MaxLength(500)]
        public string ThreatAssessment { get; set; }
        
        public bool IsArmed { get; set; }
        
        public bool HasLegalAuthority { get; set; }
        
        [MaxLength(300)]
        public string CommunicationProtocols { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}