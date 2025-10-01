using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class FoodSupply
    {
        [Key]
        public int FoodSupplyId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FoodType { get; set; } = string.Empty;
        
        public int Quantity { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public string StorageCondition { get; set; } = string.Empty;
        
        public bool IsContaminated { get; set; }
        
        public int CaloriesPerUnit { get; set; }
        
        // Foreign Keys
        public int? SafeHouseId { get; set; }
        public int? SupplyDropId { get; set; }
        
        // Navigation Properties
        public SafeHouse? SafeHouse { get; set; }
        public SupplyDrop? SupplyDrop { get; set; }
    }
}