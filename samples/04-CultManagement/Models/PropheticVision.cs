using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class PropheticVision
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        [MaxLength(2000)]
        public string VisionContent { get; set; }
        
        public DateTime ReceivedDate { get; set; }
        
        [MaxLength(100)]
        public string Prophet { get; set; }
        
        [MaxLength(500)]
        public string Interpretation { get; set; }
        
        public bool HasComeTrue { get; set; }
        
        public DateTime? FulfillmentDate { get; set; }
        
        [MaxLength(100)]
        public string Urgency { get; set; }
        
        [MaxLength(300)]
        public string RequiredActions { get; set; }
        
        public bool IsSharedWithFollowers { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<ChosenOne> RelatedChosenOnes { get; set; } = new List<ChosenOne>();
    }
}