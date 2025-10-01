using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class WeaponCache
    {
        [Key]
        public int WeaponCacheId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;
        
        public string WeaponTypes { get; set; } = string.Empty;
        
        public int TotalWeapons { get; set; }
        
        public bool IsSecured { get; set; }
        
        public DateTime LastInventoryDate { get; set; }
        
        public string AccessCode { get; set; } = string.Empty;
        
        // Foreign Keys
        public int? SafeHouseId { get; set; }
        public int? MilitaryBaseId { get; set; }
        
        // Navigation Properties
        public SafeHouse? SafeHouse { get; set; }
        public MilitaryBase? MilitaryBase { get; set; }
        public ICollection<AmmunitionStockpile> AmmunitionStockpiles { get; set; } = new List<AmmunitionStockpile>();
    }
}