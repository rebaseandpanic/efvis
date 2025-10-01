using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieApocalypse.Models
{
    public class AbandonedHospital
    {
        [Key]
        public int AbandonedHospitalId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public int Floors { get; set; }
        
        public bool HasPower { get; set; }
        
        public bool IsInfested { get; set; }
        
        public DateTime AbandonedDate { get; set; }
        
        // Foreign Key
        public int? DeadCityId { get; set; }
        
        // Navigation Properties
        public DeadCity? DeadCity { get; set; }
        public ICollection<MedicalSupply> MedicalSupplies { get; set; } = new List<MedicalSupply>();
    }
}