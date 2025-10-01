using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class FinancialAudit
    {
        public int Id { get; set; }
        
        public DateTime AuditDate { get; set; }
        
        [MaxLength(200)]
        public string AuditorName { get; set; }
        
        [MaxLength(100)]
        public string AuditType { get; set; }
        
        [MaxLength(100)]
        public string Scope { get; set; }
        
        [MaxLength(2000)]
        public string Findings { get; set; }
        
        [MaxLength(500)]
        public string Recommendations { get; set; }
        
        [MaxLength(100)]
        public string OverallRating { get; set; }
        
        public bool ComplianceIssuesFound { get; set; }
        
        [MaxLength(1000)]
        public string ComplianceIssues { get; set; }
        
        [MaxLength(500)]
        public string CorrectiveActions { get; set; }
        
        public DateTime? FollowUpDate { get; set; }
        
        public int? BankAccountId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual BankAccount BankAccount { get; set; }
    }
}