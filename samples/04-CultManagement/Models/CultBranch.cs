using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class CultBranch
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Location { get; set; }
        
        public DateTime EstablishedDate { get; set; }
        
        public int MemberCount { get; set; }
        
        [MaxLength(100)]
        public string BranchLeader { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        public decimal MonthlyRevenue { get; set; }
        
        [MaxLength(500)]
        public string SpecialActivities { get; set; }
        
        public bool IsMainBranch { get; set; }
        
        public int CultId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Cult Cult { get; set; }
        public virtual ICollection<CompoundLocation> Locations { get; set; } = new List<CompoundLocation>();
        public virtual ICollection<TrainingFacility> TrainingFacilities { get; set; } = new List<TrainingFacility>();
    }
}