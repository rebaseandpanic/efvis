using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class PropagandaMaterial
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }
        
        [MaxLength(2000)]
        public string Content { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        [MaxLength(100)]
        public string TargetAudience { get; set; }
        
        [MaxLength(500)]
        public string PsychologicalTriggers { get; set; }
        
        [MaxLength(300)]
        public string DistributionMethod { get; set; }
        
        public int EffectivenessRating { get; set; }
        
        [MaxLength(200)]
        public string Author { get; set; }
        
        public bool IsForExternalUse { get; set; }
        
        public int? SacredTextId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual SacredText SacredText { get; set; }
    }
}