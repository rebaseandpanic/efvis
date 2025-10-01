using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class SuccessionPlan
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? ActivationDate { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        public int Priority { get; set; }
        
        [MaxLength(500)]
        public string TriggerConditions { get; set; }
        
        public bool IsActive { get; set; }
        
        public int CurrentLeaderId { get; set; }
        public int SuccessorId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual CultLeader CurrentLeader { get; set; }
        public virtual CultLeader Successor { get; set; }
    }
}