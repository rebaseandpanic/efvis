using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class PropertyDeed
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string PropertyAddress { get; set; }
        
        [MaxLength(100)]
        public string PropertyType { get; set; }
        
        [MaxLength(100)]
        public string DeedNumber { get; set; }
        
        public DateTime PurchaseDate { get; set; }
        
        public decimal PurchasePrice { get; set; }
        
        public decimal CurrentValue { get; set; }
        
        [MaxLength(200)]
        public string LegalOwner { get; set; }
        
        [MaxLength(200)]
        public string ActualController { get; set; }
        
        public decimal SquareFootage { get; set; }
        
        [MaxLength(100)]
        public string ZoningType { get; set; }
        
        public bool HasMortgage { get; set; }
        
        public decimal MortgageAmount { get; set; }
        
        public int? CultAssetId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CultAsset CultAsset { get; set; }
    }
}