using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class CultAsset
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string AssetType { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public decimal EstimatedValue { get; set; }
        
        public DateTime AcquisitionDate { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        [MaxLength(100)]
        public string Condition { get; set; }
        
        public bool IsLiquid { get; set; }
        
        [MaxLength(200)]
        public string LegalOwner { get; set; }
        
        [MaxLength(500)]
        public string OwnershipDocuments { get; set; }
        
        public bool IsHidden { get; set; }
        
        public int CultId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Cult Cult { get; set; }
        public virtual ICollection<PropertyDeed> RelatedDeeds { get; set; } = new List<PropertyDeed>();
    }
}