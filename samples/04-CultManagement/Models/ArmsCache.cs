using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class ArmsCache
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string CacheName { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        [MaxLength(1000)]
        public string Inventory { get; set; }
        
        public int WeaponCount { get; set; }
        
        public int AmmunitionCount { get; set; }
        
        [MaxLength(500)]
        public string WeaponTypes { get; set; }
        
        public decimal EstimatedValue { get; set; }
        
        public bool IsConcealed { get; set; }
        
        [MaxLength(200)]
        public string AccessMethod { get; set; }
        
        [MaxLength(300)]
        public string SecurityMeasures { get; set; }
        
        public DateTime LastInventoryDate { get; set; }
        
        [MaxLength(200)]
        public string ResponsiblePerson { get; set; }
        
        public int CompoundLocationId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CompoundLocation CompoundLocation { get; set; }
    }
}