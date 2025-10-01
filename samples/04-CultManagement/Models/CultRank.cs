using System.ComponentModel.DataAnnotations;

namespace CultManagement.Models
{
    public class CultRank
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public int Level { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(300)]
        public string Requirements { get; set; }
        
        [MaxLength(300)]
        public string Privileges { get; set; }
        
        public decimal MinimumContribution { get; set; }
        
        public bool CanRecruitOthers { get; set; }
        
        public bool HasVotingRights { get; set; }
        
        [MaxLength(200)]
        public string Insignia { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Follower> Followers { get; set; } = new List<Follower>();
        public virtual ICollection<PowerStructure> PowerStructures { get; set; } = new List<PowerStructure>();
    }
}