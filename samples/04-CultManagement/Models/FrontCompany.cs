using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class FrontCompany
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }
        
        [MaxLength(100)]
        public string BusinessType { get; set; }
        
        [MaxLength(500)]
        public string OfficialDescription { get; set; }
        
        [MaxLength(500)]
        public string ActualPurpose { get; set; }
        
        public DateTime IncorporationDate { get; set; }
        
        [MaxLength(200)]
        public string RegisteredAddress { get; set; }
        
        [MaxLength(100)]
        public string TaxId { get; set; }
        
        [MaxLength(200)]
        public string NominalOwner { get; set; }
        
        [MaxLength(200)]
        public string ActualController { get; set; }
        
        public decimal AnnualRevenue { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public virtual ICollection<MoneyLaundering> MoneyLaunderingOperations { get; set; } = new List<MoneyLaundering>();
    }
}