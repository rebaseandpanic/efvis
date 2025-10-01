using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class CompoundLocation
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Address { get; set; }
        
        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }
        
        public decimal AreaInAcres { get; set; }
        
        [MaxLength(100)]
        public string TerrainType { get; set; }
        
        public int BuildingCount { get; set; }
        
        public int MaxOccupancy { get; set; }
        
        public int CurrentOccupancy { get; set; }
        
        [MaxLength(100)]
        public string SecurityLevel { get; set; }
        
        public bool HasPerimeterFencing { get; set; }
        
        [MaxLength(500)]
        public string Facilities { get; set; }
        
        public bool IsHidden { get; set; }
        
        public int CultId { get; set; }
        public int? CultBranchId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Cult Cult { get; set; }
        public virtual CultBranch CultBranch { get; set; }
        public virtual ICollection<ArmsCache> ArmsCaches { get; set; } = new List<ArmsCache>();
        public virtual ICollection<SurveillanceSystem> SurveillanceSystems { get; set; } = new List<SurveillanceSystem>();
        public virtual ICollection<EscapeRoute> EscapeRoutes { get; set; } = new List<EscapeRoute>();
    }
}