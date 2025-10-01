using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AccountNumber { get; set; }
        
        [MaxLength(200)]
        public string BankName { get; set; }
        
        [MaxLength(100)]
        public string AccountType { get; set; }
        
        [MaxLength(200)]
        public string AccountHolder { get; set; }
        
        public decimal Balance { get; set; }
        
        public DateTime OpenedDate { get; set; }
        
        public DateTime? ClosedDate { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        [MaxLength(200)]
        public string SignatoryAuthority { get; set; }
        
        public bool IsOffshore { get; set; }
        
        [MaxLength(100)]
        public string Country { get; set; }
        
        public bool IsMonitored { get; set; }
        
        public int? FrontCompanyId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual FrontCompany FrontCompany { get; set; }
        public virtual ICollection<FinancialAudit> FinancialAudits { get; set; } = new List<FinancialAudit>();
    }
}