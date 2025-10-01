using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class Helicopter
    {
        [Key]
        public int HelicopterId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string CallSign { get; set; } = string.Empty;
        
        public string Model { get; set; } = string.Empty;
        
        public int FuelLevel { get; set; }
        
        public string Status { get; set; } = string.Empty; // Operational, Damaged, Crashed, Missing
        
        public string CurrentLocation { get; set; } = string.Empty;
        
        public int PassengerCapacity { get; set; }
        
        // Foreign Key
        public int? MilitaryBaseId { get; set; }
        
        // Navigation Properties
        public MilitaryBase? MilitaryBase { get; set; }
        public ICollection<EvacuationPoint> ServedEvacuationPoints { get; set; } = new List<EvacuationPoint>();
        public ICollection<SupplyDrop> SupplyDrops { get; set; } = new List<SupplyDrop>();
    }
}