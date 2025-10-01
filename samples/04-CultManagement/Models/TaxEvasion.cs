using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class TaxEvasion
    {
        public int Id { get; set; }
        
        public int TaxYear { get; set; }
        
        [MaxLength(100)]
        public string TaxType { get; set; }
        
        public decimal LegalTaxOwed { get; set; }
        
        public decimal ActualTaxPaid { get; set; }
        
        public decimal AmountEvaded { get; set; }
        
        [MaxLength(500)]
        public string EvasionMethod { get; set; }
        
        [MaxLength(1000)]
        public string Documentation { get; set; }
        
        public bool WasDetected { get; set; }
        
        public DateTime? DetectionDate { get; set; }
        
        [MaxLength(500)]
        public string Penalties { get; set; }
        
        public decimal PenaltyAmount { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}