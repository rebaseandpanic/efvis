using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class InvestmentPortfolio
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string PortfolioName { get; set; }
        
        [MaxLength(100)]
        public string PortfolioType { get; set; }
        
        public decimal TotalValue { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        [MaxLength(200)]
        public string PortfolioManager { get; set; }
        
        [MaxLength(100)]
        public string RiskLevel { get; set; }
        
        public decimal AnnualReturn { get; set; }
        
        [MaxLength(500)]
        public string InvestmentStrategy { get; set; }
        
        [MaxLength(1000)]
        public string Holdings { get; set; }
        
        public bool IsOffshore { get; set; }
        
        [MaxLength(100)]
        public string Jurisdiction { get; set; }
        
        public bool IsHidden { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}