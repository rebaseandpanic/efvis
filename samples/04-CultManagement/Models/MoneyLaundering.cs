using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class MoneyLaundering
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string OperationName { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public decimal OriginalAmount { get; set; }
        
        public decimal CleanedAmount { get; set; }
        
        [MaxLength(100)]
        public string Method { get; set; }
        
        [MaxLength(500)]
        public string LayeringProcess { get; set; }
        
        [MaxLength(200)]
        public string SourceOfFunds { get; set; }
        
        [MaxLength(200)]
        public string FinalDestination { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        public bool WasDetected { get; set; }
        
        public int? FrontCompanyId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual FrontCompany FrontCompany { get; set; }
    }
}