using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class SupplyDrop
    {
        [Key]
        public int SupplyDropId { get; set; }
        
        public DateTime DropDate { get; set; }
        
        public string DropLocation { get; set; } = string.Empty;
        
        public string Contents { get; set; } = string.Empty;
        
        public bool IsRecovered { get; set; }
        
        public string DropCode { get; set; } = string.Empty;
        
        public int PriorityLevel { get; set; }
        
        // Foreign Keys
        public int? MilitaryBaseId { get; set; }
        public int? HelicopterId { get; set; }
        
        // Navigation Properties
        public MilitaryBase? MilitaryBase { get; set; }
        public Helicopter? Helicopter { get; set; }
        public ICollection<FoodSupply> FoodSupplies { get; set; } = new List<FoodSupply>();
        public ICollection<MedicalSupply> MedicalSupplies { get; set; } = new List<MedicalSupply>();
    }
}