using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class EscapeRoute
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string RouteName { get; set; }
        
        [MaxLength(1000)]
        public string RouteDescription { get; set; }
        
        [MaxLength(200)]
        public string StartPoint { get; set; }
        
        [MaxLength(200)]
        public string EndPoint { get; set; }
        
        public decimal DistanceInMiles { get; set; }
        
        public TimeSpan EstimatedTravelTime { get; set; }
        
        [MaxLength(100)]
        public string TransportationMethod { get; set; }
        
        [MaxLength(500)]
        public string RouteHazards { get; set; }
        
        [MaxLength(500)]
        public string SafetyMeasures { get; set; }
        
        public bool RequiresSpecialEquipment { get; set; }
        
        [MaxLength(300)]
        public string EquipmentRequired { get; set; }
        
        public DateTime LastTested { get; set; }
        
        public bool IsOperational { get; set; }
        
        public int CompoundLocationId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CompoundLocation CompoundLocation { get; set; }
    }
}