using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class MedicalSupply
    {
        [Key]
        public int MedicalSupplyId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string SupplyType { get; set; } = string.Empty;
        
        public int Quantity { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public string UsageInstructions { get; set; } = string.Empty;
        
        public bool RequiresPrescription { get; set; }
        
        public string StorageRequirements { get; set; } = string.Empty;
        
        // Foreign Keys
        public int? AbandonedHospitalId { get; set; }
        public int? SupplyDropId { get; set; }
        
        // Navigation Properties
        public AbandonedHospital? AbandonedHospital { get; set; }
        public SupplyDrop? SupplyDrop { get; set; }
    }
}