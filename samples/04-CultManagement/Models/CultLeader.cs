using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class CultLeader
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        [MaxLength(100)]
        public string Title { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(500)]
        public string Background { get; set; }
        
        public DateTime JoinedDate { get; set; }
        
        public DateTime? AppointedLeaderDate { get; set; }
        
        [MaxLength(200)]
        public string SpecialAbilities { get; set; }
        
        public bool IsSupremeLeader { get; set; }
        
        public int InfluenceLevel { get; set; }
        
        public int CultId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Cult Cult { get; set; }
        public virtual ICollection<InnerCircle> InnerCircleMembers { get; set; } = new List<InnerCircle>();
        public virtual ICollection<SuccessionPlan> SuccessionPlans { get; set; } = new List<SuccessionPlan>();
        public virtual ICollection<Ritual> LeadRituals { get; set; } = new List<Ritual>();
    }
}