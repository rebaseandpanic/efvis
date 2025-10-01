using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class AmmunitionStockpile
    {
        [Key]
        public int AmmunitionStockpileId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string AmmoType { get; set; } = string.Empty;
        
        public int Quantity { get; set; }
        
        public string Caliber { get; set; } = string.Empty;
        
        public bool IsExplosive { get; set; }
        
        public DateTime ManufactureDate { get; set; }
        
        public string StorageCondition { get; set; } = string.Empty;
        
        // Foreign Key
        public int? WeaponCacheId { get; set; }
        
        // Navigation Properties
        public WeaponCache? WeaponCache { get; set; }
    }
}