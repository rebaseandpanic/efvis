using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class SafeHouse
    {
        [Key]
        public int SafeHouseId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;
        
        public int Capacity { get; set; }
        
        public int SecurityLevel { get; set; }
        
        public bool HasPower { get; set; }
        
        public bool HasWaterSupply { get; set; }
        
        public DateTime EstablishedDate { get; set; }
        
        // Navigation Properties
        public ICollection<Survivor> Survivors { get; set; } = new List<Survivor>();
        public ICollection<WeaponCache> WeaponCaches { get; set; } = new List<WeaponCache>();
        public ICollection<FoodSupply> FoodSupplies { get; set; } = new List<FoodSupply>();
        public ICollection<TrapSystem> TrapSystems { get; set; } = new List<TrapSystem>();
    }
}