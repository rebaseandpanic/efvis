using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SafeHouse
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string CodeName { get; set; }
        
        [MaxLength(300)]
        public string Address { get; set; }
        
        [MaxLength(100)]
        public string CoverStory { get; set; }
        
        public int MaxCapacity { get; set; }
        
        [MaxLength(500)]
        public string Amenities { get; set; }
        
        [MaxLength(300)]
        public string SecurityFeatures { get; set; }
        
        [MaxLength(500)]
        public string SupplyCache { get; set; }
        
        public bool IsCurrentlyOccupied { get; set; }
        
        public int CurrentOccupants { get; set; }
        
        [MaxLength(200)]
        public string ContactPerson { get; set; }
        
        [MaxLength(100)]
        public string EmergencyProtocol { get; set; }
        
        public bool IsCompromised { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}