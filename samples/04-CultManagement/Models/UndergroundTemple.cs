using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class UndergroundTemple
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Location { get; set; }
        
        public decimal DepthInFeet { get; set; }
        
        public decimal FloorArea { get; set; }
        
        public int NumberOfChambers { get; set; }
        
        [MaxLength(500)]
        public string Architecture { get; set; }
        
        [MaxLength(500)]
        public string SacredItems { get; set; }
        
        [MaxLength(300)]
        public string AccessMethods { get; set; }
        
        public bool HasVentilation { get; set; }
        
        public bool HasElectricity { get; set; }
        
        [MaxLength(500)]
        public string SecurityMeasures { get; set; }
        
        public int MaxCapacity { get; set; }
        
        public DateTime LastUsedDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}